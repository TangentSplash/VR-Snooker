using UnityEngine;
using System.Collections.Generic;

public class Referee : MonoBehaviour
{
    public bool Player1 = true;
    public bool RedRemaining = true;
    public string Expected = "Red";
    public Score Scoreboard;
    public GameObject pointer;
    public GameObject Line;
    public List<string> BallOrder = new List<string>() { "Yellow","Green","Brown","Blue", "Pink","Black" };

    public bool RedExpected = true;
    bool potthisshot = false;

    Collider PottedBall = null;

    public void Potted(Collider Ball)
    {
        PottedBall = Ball;
        string name= Ball.gameObject.name.Split(' ')[0];
        Debug.Log(name);
        int score = Ball.gameObject.GetComponent<Ball>().Score;

        if (Expected == name)
        {
            Scoreboard.PlayerScored(Player1, score);
            Debug.Log("Potted");
            potthisshot = true;
            if (RedRemaining)
            {
                RedExpected = !RedExpected;
                Debug.Log("Red Expected " + RedExpected);
                if(!RedExpected)
                {
                    pointer.SetActive(true);
                }
                else
                {
                    Expected = "Red";
                    Ball.gameObject.GetComponent<Ball>().Reset();
                }
            }
            else
            {
                if (BallOrder.Count > 0)
                {
                    Expected = BallOrder[0];
                    BallOrder.RemoveAt(0);
                }
            }
        }
        else
        {
            Debug.Log("Wrong Ball FOUL");
            Ball.gameObject.GetComponent<Ball>().Reset();
            potthisshot = false;
        }
    }

    public void AllStopped()
    {
        if (!potthisshot)
        {
            NextPlayer();
        }
        potthisshot = false;
    }

    void NextPlayer()
    {
        if (Time.time>5)
        {
            Player1 = !Player1;
            Scoreboard.PlayerChanged(Player1);
            if (Player1)
            {
                Line.SetActive(true);
            }
            else
            {
                Line.SetActive(false);
            }
            if(RedRemaining)
            {
                RedExpected = true;
                Expected = "Red";
            }
        }
    }

    public void ChooseBall(string ball)
    {
        Expected = ball;
        pointer.SetActive(false);
    }

    public void BallOut(Collider Ball)
    {
        if (Ball==PottedBall)
        {
            PottedBall = null;
        }
        else
        {
            Debug.Log("Out of Bounds FOUL");
            Ball.gameObject.GetComponent<Ball>().Reset();
            potthisshot = false;
        }

    }
}
