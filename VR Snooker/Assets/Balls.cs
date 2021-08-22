using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class Balls : MonoBehaviour
{
    public GameObject ball;
    public List<snookerballinfo> ballinfos;
    public Texture2D[] Numbers;
    public Color[] Colours;

    int NoBalls = 15;
    Material Material;

    int NeedthisRow = 1;
    int HavethisRow = 0;
    float row;
    Vector3 StartPos;

    public void Place(float scalefactor,bool Pool)
    {
        if (Pool)
        {
            float scale=0.057f;
            List<GameObject> balls= new List<GameObject>();
            row = -0.905f*scalefactor;
            for (int i = 0; i < NoBalls; i++)
            {
                StartPos = new Vector3(0, 0, 0);
                GameObject instance = Instantiate(ball, gameObject.transform);
                instance.name = (i + 1).ToString();
                Material = instance.GetComponent<MeshRenderer>().material;
                Material.SetInt("Boolean_e1fb8e839d384bb391442c5a4c26f222", 1);                                 //Pool Ball
                Material.SetColor("Color_9c26e1ea30de4dfe92fccc2b500228b3", Colours[i % ((NoBalls + 1) / 2)]);  //Colour
                Material.SetTexture("Texture2D_080d4b86d4be4d578a2f8e9d3bd90694", Numbers[i]);                  //Number
                Material.SetInt("Boolean_ae177396764e438fab2d176da88c4343", Convert.ToInt32(i > NoBalls / 2));  //Stripe
                balls.Add(instance);
            }
            int number=0;
            List<int> Completed = new List<int>() { 7 };
            for (int i=0;i<NoBalls;i++)
            {
                if (i==4)
                {
                    number = 7;
                }
                else
                {
                    do
                    {
                        number = UnityEngine.Random.Range(0, 15);
                    }
                    while (Completed.FindIndex(x => x.Equals(number)) != -1);
                    Completed.Add(number);
                }
                Triangle(ref StartPos, scale);
                balls[number].transform.localPosition = StartPos;
                
            }
            BuildSnookerBall(ballinfos[0], scalefactor);
        }
        else
        {
            foreach (snookerballinfo ballinfo in ballinfos)
            {
                BuildSnookerBall(ballinfo,scalefactor);
            }
        }
    }

    void BuildSnookerBall(snookerballinfo ballinfo,float scalefactor)
    {
        string name = ballinfo.name;
        int total = 1;
        float mass = 0;
        float scale;
        StartPos = ballinfo.Pos;
        if (scalefactor >= 0.75)
        {
            scale = 0.0525f;
            if (name == "Red")
            {
                total = 15;
            }
        }
        else
        {
            scale = 0.0475f;
            mass = 0.1f;
            if (name == "Red")
            {
                total = 10;
            }
        }
        StartPos.x *= scalefactor;
        row = StartPos.x;
        if (name == "Green" || name == "Yellow" || name == "Cue Ball")
        {
            StartPos.z *= scalefactor;
        }
        for (int i = 0; i < total; i++)
        {
            if (ballinfo.name == "Red")
            {
                name = ballinfo.name + " " + i;
                Triangle(ref StartPos, scale);
            }
            GameObject instance = Instantiate(ball, gameObject.transform);
            Ball traits = instance.GetComponent<Ball>();
            instance.name = name;
            instance.transform.localScale = scale * Vector3.one;
            instance.transform.localPosition = StartPos;
            Material = instance.GetComponent<MeshRenderer>().material;
            Material.SetInt("Boolean_e1fb8e839d384bb391442c5a4c26f222", 0);                //Pool Ball
            Material.SetColor("Color_9c26e1ea30de4dfe92fccc2b500228b3", ballinfo.colour);  //Colour
                                                                                           //instance.GetComponent<MeshRenderer>().material = ballinfo.material;
            traits.Score = ballinfo.score;
            traits.StartPos = StartPos;
            if (ballinfo.name == "Cue Ball")
            {
                instance.AddComponent<XRGrabInteractable>().throwOnDetach = false;
                instance.GetComponent<Rigidbody>().mass = 0.17f;
            }
            if (mass != 0)
            {
                instance.GetComponent<Rigidbody>().mass = mass;
            }
        }
    }

    void Triangle(ref Vector3 StartPos,float scale)
    {
        if (HavethisRow == NeedthisRow)
        {
            HavethisRow = 0;
            NeedthisRow++;
            row -= (scale / Mathf.Sqrt(2));
        }
        StartPos.z = (HavethisRow - (NeedthisRow / 2f) + 0.5f) * scale;
        StartPos.x = row;
        HavethisRow++;
    }
}

[System.Serializable]
public class snookerballinfo
{
    public string name;
    public Material material;
    public Color colour;
    public int score;
    public Vector3 Pos;
}
