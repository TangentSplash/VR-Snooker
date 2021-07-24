using UnityEngine;

public class MoveRigidbody : MonoBehaviour
{
    public Transform BaseHand;
    public Transform FrontHand;

    public Rigidbody CueRigidbody;

    public float MaxDistanceDelta = 10;
    public float MinAngleMove = 0.01f;
    public float MinDistMove = 1;

    private Vector3 Target;
    private Vector3 Current;
    private Vector3 Delta;

    private Vector3 velocityDelta;
    private Vector3 CalculatedVelocity;

    private Quaternion TargetQuat;
    private Quaternion CurrentQuat;
    private Quaternion DeltaQuat;
    private Vector3 HandDelta;

    void FixedUpdate()
    {
        Target = BaseHand.position;
        Current = transform.position;
        Delta = Target - Current;
        if (Delta.magnitude > MinDistMove)
        { 
            //CueRigidbody.MovePosition(Target);
            velocityDelta = Delta / Time.deltaTime;
            CalculatedVelocity = Vector3.MoveTowards(CueRigidbody.velocity, velocityDelta, MaxDistanceDelta);
            CueRigidbody.velocity = CalculatedVelocity;
        }
        HandDelta = FrontHand.position - Current;
        CurrentQuat = transform.rotation;
        TargetQuat = Quaternion.LookRotation(HandDelta, Vector3.up);
        //transform.rotation = TargetQuat;
        DeltaQuat = TargetQuat * Quaternion.Inverse(CurrentQuat);
        DeltaQuat.ToAngleAxis(out float angle, out Vector3 axis);
        angle = (angle > 180f) ? angle - 360f : angle;
        //Debug.LogWarning(angle + " " + axis);
        if ((angle>MinAngleMove || angle<-MinAngleMove) && angle<float.PositiveInfinity)
        {
            //CueRigidbody.MoveRotation(TargetQuat);
            Vector3 angularTarget = angle * axis;
            Vector3 calculatedAngularVelocity = Vector3.MoveTowards(CueRigidbody.angularVelocity, angularTarget, MaxDistanceDelta);
            if (!float.IsNaN(calculatedAngularVelocity.x) && !float.IsNaN(calculatedAngularVelocity.y) && !float.IsNaN(calculatedAngularVelocity.z))
            {
                CueRigidbody.angularVelocity = calculatedAngularVelocity;
            }
        }
        
    }
    public void LateUpdate()
    {
        CueRigidbody.Sleep();
    }
}
