using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public ActionBasedController RightTeleportRay;
    public bool isPressed;
    public float activationThreshold = 0.1f;

    void Start()
    {
        RightTeleportRay.selectAction.action.performed += Action_performed;
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext Button)
    {
        if(Button.ReadValue<float>()>=activationThreshold)
        {
            RightTeleportRay.gameObject.SetActive(true);
        }
        else
        {
            RightTeleportRay.gameObject.SetActive(false);
        }
    }

    void Update()
    {

    }
}
