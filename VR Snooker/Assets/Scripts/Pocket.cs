using UnityEngine;

public class Pocket : MonoBehaviour
{
    public Referee referee;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Pot");
        if (collider.gameObject.name == "White")
        {
            
        }
        else if (collider.gameObject.layer == 15)
        {
            Debug.Log("Pot Ball");
            referee.Potted(collider);
        }
    }
}
