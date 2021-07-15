using UnityEngine;

public class ChoseBall : MonoBehaviour
{
    public Referee referee;

    public void Chose(string ball)
    {
        referee.ChooseBall(ball);
    }
}
