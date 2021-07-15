using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCue : MonoBehaviour
{
    public GameObject Pivot;
    Rigidbody Rigidbody;
    Cue FollowScript;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        FollowScript = Pivot.GetComponent<Cue>();
        transform.localPosition = new Vector3(0, -1, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        FollowScript.Follow = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        FollowScript.Follow = false;
        Rigidbody.AddRelativeForce(0, 2, 0, ForceMode.Impulse);
    }
    private void OnMouseUp()
    {
        transform.localPosition = new Vector3(0, -1, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        FollowScript.Follow = true;
    }
}
