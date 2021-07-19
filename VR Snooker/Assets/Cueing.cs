using System.Collections;
using System.Collections.Generic;
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


    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == 15 || collider.gameObject.name == "Cue Ball")
        {
            Velocity = Rigidbody.velocity;
            Pos=collider.GetContact(0).point;
            Debug.Log(Velocity + " " + Pos);
            collider.rigidbody.AddForceAtPosition(Velocity, Pos, ForceMode.Impulse);
        }
    }

    /*private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer==15 || collider.gameObject.name=="Cue Ball")
        {
            Velocity = Rigidbody.velocity;
            //GetContacts()
            //collider.attachedRigidbody.AddForceAtPosition()
        }
    }*/
}
