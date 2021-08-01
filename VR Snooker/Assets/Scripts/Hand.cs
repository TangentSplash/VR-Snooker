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
    public MoveRigidbody Cue;

    void Start()
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
    }

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
        if(Grabbing && InColllider)
        {
            Grabbing = false;
            Cue.Hook();
        }
    }

}
