using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    int P1Score;
    int P2Score;
    TMP_Text P1ScoreDisp;
    TMP_Text P2ScoreDisp;

    TMP_Text P1Name;
    TMP_Text P2Name;
    
    
    void Start()
    {
        TMP_Text[] TextBoxes = GetComponentsInChildren<TMP_Text>();
        P1Name = TextBoxes[0];
        P2Name = TextBoxes[1];
        P1ScoreDisp = TextBoxes[2];
        P2ScoreDisp = TextBoxes[3];
    }

    public void PlayerScored(bool player1, int score)
    {
        if (player1)
        {
            P1Score += score;
            P1ScoreDisp.text = P1Score.ToString();
        }
        else
        {
            P2Score += score;
            P2ScoreDisp.text = P2Score.ToString();
        }
    }

    public void PlayerChanged(bool Player1)
    {
        if (Player1)
        {
            P1Name.fontStyle = FontStyles.Bold | FontStyles.Underline;
            P2Name.fontStyle = FontStyles.Normal;
        }
        else
        {
            P2Name.fontStyle = FontStyles.Bold | FontStyles.Underline;
            P1Name.fontStyle = FontStyles.Normal;
        }

    }
}
