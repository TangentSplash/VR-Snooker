using UnityEngine;

public class GrabbedCueBall : MonoBehaviour
{
    public bool grabbedlast;
    private bool grabbed;
    private bool grabbedlate;

    public void Grabbed(bool grab)
    {
        Debug.Log("Grabbed" + grab);
        grabbed = grab;
    }

    private void LateUpdate()
    {
        if (grabbedlast!=grabbedlate)
        {
            grabbedlast = grabbed;
        }
        grabbedlate = grabbed;
    }
}
