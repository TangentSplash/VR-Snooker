using System;
using UnityEngine;

public class Cue : MonoBehaviour
{
    public Transform CueBall;
    public Transform Hand;
    public Transform StickContainer;

    public Transform StickPivot;
    public GameObject Stick;

    public GameObject Line;
    public GameObject ReboundLine;
    LineRenderer LineRender;
    LineRenderer ReboundLineRender;

    public bool Follow = true;

    GameObject Collided;


    private float dist = 0.15f;
    RaycastHit hit;
    void Start()
    {
        Vector3 pos = Stick.transform.localPosition;
        pos.y -= dist;
        Stick.transform.localPosition=new Vector3(pos.x,pos.y,pos.z);
        //Debug.Log(Stick.transform.localPosition);
        StickPivot.rotation = Quaternion.Euler(0, 90, 90);
        LineRender=Line.GetComponent<LineRenderer>();
        ReboundLineRender = ReboundLine.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Follow)
        {

            Vector2 BallPoint = new Vector2(CueBall.position.x, CueBall.position.z);
            Vector2 HandPoint = new Vector2(Hand.position.x, Hand.position.z);
            Vector2 StickVector = BallPoint - HandPoint;
            Vector3 RayVector =new Vector3(StickVector.x,0,StickVector.y);
            double Angle = -Math.Atan2(StickVector.x, StickVector.y);
            Angle *= (180 / Math.PI);
            Angle -= 90;
            Physics.Raycast(CueBall.position, RayVector, out hit);
            LineRender.SetPosition(0, CueBall.position);
            LineRender.SetPosition(1, hit.point);
            Collided = hit.collider.gameObject;
            if (Collided.layer==15)
            {
                ReboundLine.SetActive(true);
                Vector3 Rebound = hit.point - Collided.transform.position;
                Rebound = Vector3.Cross(Rebound,new Vector3(0,1,0));
                Rebound *= 3;
                ReboundLineRender.SetPosition(0, Collided.transform.position);
                ReboundLineRender.SetPosition(1, Collided.transform.position+Rebound);
                ReboundLineRender.SetPosition(2, Collided.transform.position + (Rebound*-1));
            }
            else
            {
                ReboundLine.SetActive(false);
            }
            StickContainer.position = CueBall.position;
            //float Angle= Vector2.Angle(new Vector2(1, 0), StickVector);
            StickPivot.rotation = Quaternion.Euler(0, -(float)Angle, 96);
            //Debug.Log(Angle);
        }
    }
}
