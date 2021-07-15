using UnityEngine;

public class GuideLine : MonoBehaviour
{

    LineRenderer Line;
    public MoveRigidbody Cue;
    public Vector3 RayVector;
    RaycastHit hit;
    Transform BaseHand;
    Vector3 BaseHandPos;
    Transform FrontHand;
    Vector3 FrontHandPos;
    float ypos;

    void Start()
    {
        Line = GetComponent<LineRenderer>();
        BaseHand = Cue.BaseHand;
        FrontHand = Cue.FrontHand;
        ypos = transform.position.y;
    }

    void Update()
    {
        BaseHandPos = BaseHand.position;
        FrontHandPos = FrontHand.position;
        BaseHandPos.y = ypos;
        FrontHandPos.y = ypos;
        RayVector = FrontHandPos - BaseHandPos;
        BaseHandPos += (RayVector.normalized);


        Physics.Raycast(BaseHandPos, RayVector, out hit,Mathf.Infinity,(1<<14 | 1<< 15), QueryTriggerInteraction.Ignore);
        Line.SetPosition(0, BaseHandPos);
        Line.SetPosition(1, hit.point);
    }
}
