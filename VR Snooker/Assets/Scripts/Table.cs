using UnityEngine;
using System.Collections.Generic;

public class Table : MonoBehaviour
{
    public Referee referee;
    public List<Ball> Balls;

    bool AllStopPrev=true;
    float time;
    bool instantstop = false;

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 15)
        {
            Balls.Remove(collider.GetComponent<Ball>());
            Debug.Log("OUT!");
            referee.BallOut(collider);
        }
        else if (collider.gameObject.name=="Cue Ball")
        {
            Debug.Log("White Ball Out Foul");
            collider.GetComponent<Ball>().Reset();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer==15 || collider.gameObject.name == "White")
        {
            Balls.Add(collider.GetComponent<Ball>());
        }
    }

    private void Update()
    {
        int Stopped = 0;
        foreach (Ball ball in Balls)
        {
            if(!ball.Stopped)
            {
                AllStopPrev = false;
                break;
            }
            Stopped++;
        }
        if (!instantstop)
        {
            if (Stopped == Balls.Count && !AllStopPrev)
            {
                AllStopPrev = true;
                instantstop = true;
                time = Time.time;
            }
        }
        else
        {
            if (Stopped != Balls.Count)
            {
                instantstop = false;
                Debug.Log("Not actually Stopped, Sorry!");
            }
            else if (Time.time >= time + 1)
            {
                instantstop = false;
                referee.AllStopped();

                int Red = 0;
                foreach (Ball ball in Balls)
                {
                    if (ball.gameObject.name.Split(' ')[0] == "Red")
                    {
                        Red++;
                    }
                }
                if (Red == 0)
                {
                    referee.RedRemaining = false;
                }
            }
        }        
    }    
}
