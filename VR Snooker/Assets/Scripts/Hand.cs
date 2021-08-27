using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
    [SerializeField] private InputActionReference actionReference;
    public bool InColllider;
    public bool Grabbing;
    bool LastGrab=false;
    public MoveRigidbody Cue;
    public XRBaseController controller;

    /*void Start()
    {
        actionReference.action.started += Action_started;
        actionReference.action.canceled += Action_canceled;
    }


    private void Action_started(InputAction.CallbackContext obj)
    {
        Grabbing = true;
    }

    private void Action_canceled(InputAction.CallbackContext obj)
    {
        Grabbing = false;
    }*/

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Hook")
        {
            InColllider = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Hook")
        {
            InColllider = false;
        }
    }

    void Update()
    {
        Grabbing = controller.selectInteractionState.activatedThisFrame;
        if(Grabbing && InColllider && !LastGrab)
        {
            Cue.Hook();
        }
        LastGrab = Grabbing;
    }

    public void Test()
    {
        Debug.Log("Selected!!!!");
    }

}
