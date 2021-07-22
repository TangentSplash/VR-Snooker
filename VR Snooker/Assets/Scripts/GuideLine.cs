using UnityEngine;

public class GuideLine : MonoBehaviour
{

    LineRenderer Line;
    public MoveRigidbody Cue;
    public Vector3 RayVector;
    RaycastHit hit;
    RaycastHit hit2;
    Transform BaseHand;
    Vector3 BaseHandPos;
    Transform FrontHand;
    Vector3 FrontHandPos;
    Vector3 Centre;
    public Vector3 ReboundVector;
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


        Physics.Raycast(BaseHandPos, RayVector, out hit,Mathf.Infinity,1<<14 | 1<< 15, QueryTriggerInteraction.Ignore);
        Line.SetPosition(0, BaseHandPos);
        Line.SetPosition(1, hit.point);
        Centre = hit.transform.position;
        ReboundVector= Centre-hit.point;
        ReboundVector.y = 0;
        Physics.Raycast(hit.point, ReboundVector, out hit2, Mathf.Infinity, 1 << 14 | 1 << 15, QueryTriggerInteraction.Ignore);
        //Debug.Log(hit2.point);
        Line.SetPosition(2, hit2.point+ReboundVector.normalized);
    }
}
