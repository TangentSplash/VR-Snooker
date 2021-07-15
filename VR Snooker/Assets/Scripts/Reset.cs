using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject Confetti;

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "White")
        {
            Debug.Log("Out");   
            collider.attachedRigidbody.Sleep();
            collider.transform.position = new Vector3(1.174f, 0.815f, 0.13f);
        }
        else if(collider.gameObject.layer==15)
        {
            //collider.GetComponent<Ball>().Reset();
        }
    }
}
