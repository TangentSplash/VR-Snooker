using UnityEngine;

public class Cueing : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private Vector3 Velocity;
    private Vector3 Pos;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }


    /*private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == 15 || collider.gameObject.name == "Cue Ball")
        {
            Velocity = Rigidbody.velocity;
            Pos=collider.GetContact(0).point;
            Debug.Log(Velocity + " " + Pos);
            collider.rigidbody.AddForceAtPosition(Velocity, Pos, ForceMode.Impulse);
        }
    }*/

    /*private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer==15 || collider.gameObject.name=="Cue Ball")
        {
            Velocity=Rigidbody.a
            Velocity = Rigidbody.velocity;
            Pos =collider.ClosestPointOnBounds(GetComponentsInChildren<Transform>()[1].position);
            Debug.Log(Velocity + " " + Pos);
            collider.attachedRigidbody.AddForceAtPosition(Velocity, Pos, ForceMode.VelocityChange);
        }
    }*/
}
