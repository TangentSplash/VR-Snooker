using UnityEngine;

public class CueMovement : MonoBehaviour
{
    public Transform RightHand;
    public Transform LeftHand;
    public GameObject Stick;
    public GameObject HitStick;
    public GameObject RidgidbodyContainer;
    public Transform OtherRigidbody;

    public bool hit=false;

    void Update()
    {
        if (!hit)
        {
            float x = RightHand.position.x;
            float y = RightHand.position.y;
            float z = RightHand.position.z;

            Stick.transform.position = new Vector3(x, y, z);
            Stick.transform.LookAt(LeftHand, Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Hit();
        }
    }

    void Hit()
    {
        if(hit)
        {
            HitEnd();
        }
        else
        {
            HitStart();
        }
    }

    public void HitStart()
    {
        hit = true;
        HitStick.SetActive(hit);
        Stick.SetActive(!hit);
        HitStick.transform.position = Stick.transform.position;
        HitStick.transform.rotation = Stick.transform.rotation;
        RidgidbodyContainer.transform.localPosition = new Vector3(0, 0, 0);
        RidgidbodyContainer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        OtherRigidbody.localPosition = new Vector3(0, 0, 0);
        OtherRigidbody.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void HitEnd()
    {
        hit = false;
        HitStick.SetActive(hit);
        Stick.SetActive(!hit);
    }
}
