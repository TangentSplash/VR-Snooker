using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Friction : MonoBehaviour
{
    public PhysicMaterial Ball;
    public PhysicMaterial Cue;
    public PhysicMaterial Felt;
    public PhysicMaterial Table;
    public PhysicMaterial Cushion;

    public GameObject Balls;

    public GameObject[] Canvas;

    bool Set = false;
    void Start()
    {
        foreach (GameObject canvas in Canvas)
        {
            GetSlider(canvas, 0);
        }
        Debug.Log("Done");
        Set = true;
    }

    public void ValueChanged(GameObject Changed)
    {
        if (Set)
        {
            GetSlider(Changed, 1);
        }
    }

    void GetSlider(GameObject canvas,int mode)
    {
        PhysicMaterial Friction=null;
        Slider slider=null;
        TMP_Text[] text;
        slider = canvas.GetComponentInChildren<Slider>();
        Debug.Log(canvas.name);
        string[] Object = canvas.name.Split(' ');

        switch (Object[0])
        {
            case "Ball":
                Friction = Ball;
                break;
            case "Cue":
                Friction = Cue;
                break;
            case "Felt":
                Friction = Felt;
                break;
            case "Table":
                Friction = Table;
                break;
            case "Cushion":
                Friction = Cushion;
                break;
            case "Drag":
                Rigidbody[] Rigidbodies = Balls.GetComponentsInChildren<Rigidbody>();
                if (mode == 0)
                {
                    slider.value = Rigidbodies[0].drag;
                }
                else
                {
                    foreach (Rigidbody Body in Rigidbodies)
                    {
                        Body.drag = slider.value;
                    }
                }
                text = canvas.GetComponentsInChildren<TMP_Text>();
                text[1].text = Math.Round(slider.value, 2).ToString();
                return;
            default:
                Debug.LogError("Error with friction: " + Object[0]);
                return;
        }

        switch (Object[1])
        {
            case "Static":
                if (mode == 0)
                {
                    slider.value = Friction.staticFriction;
                    Debug.Log(Friction);
                }
                else
                {
                    Friction.staticFriction = slider.value;
                }
                break;
            case "Dynamic":
                if (mode == 0)
                {
                    slider.value = Friction.dynamicFriction;
                }
                else
                {
                    Friction.dynamicFriction = slider.value;
                }
                break;
            case "Bounce":
                if (mode == 0)
                {
                    slider.value = Friction.bounciness;
                }
                else
                {
                    Friction.bounciness = slider.value;
                }
                break;
            default:
                Debug.LogError("Error with friction: " + Object[1]);
                break;
        }
        text = canvas.GetComponentsInChildren<TMP_Text>();
        text[1].text = Math.Round(slider.value,2).ToString();
        Debug.Log(slider.value);
    }

    public void ResetBalls()
    {
        Ball[] BallClass = Balls.GetComponentsInChildren<Ball>();
        foreach (Ball Ball in BallClass)
        {
            Ball.Reset();
        }
    }
}
