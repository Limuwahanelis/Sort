using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class SorterRigController : MonoBehaviour
{
    public bool isRigOn;
    Vector3 PosToMoveRighthand;
    Vector3 PosToMoveLefthand;
    public Transform targetR;
    public Transform targetL;
    public Sorter sorter;
    public float rigIncreaseSpeed;
    public Rig rig;
    public RigBuilder rigBuilder;

    [SerializeField]
    private MultiAimConstraint leftMultiAim;
    [SerializeField]
    private MultiAimConstraint rightMultiAim;
    [SerializeField]
    private ChainIKConstraint leftChain;
    [SerializeField]
    private ChainIKConstraint rightChain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            rigBuilder.Build();
        }
    }

    public void SetTargetsForConstraints(Transform leftHandTransform, Transform rightHandTransform)
    {
        leftChain.data.target = leftHandTransform;
        rightChain.data.target = rightHandTransform;
        rigBuilder.Build();
        StartCoroutine(increaseRigWeight());
    }
    public void SetFut(Vector3 leftHandTargetPos, Vector3 rightHandTargetPos)
    {
        PosToMoveLefthand = rightHandTargetPos;
        PosToMoveRighthand = leftHandTargetPos;
        StartCoroutine(increaseRigWeight());
    }
    public void MoveLeftContraintToNewpos(Vector3 leftHandTargetPos)
    {
        targetL.position = leftHandTargetPos;
    }
    public void MoveRightContraintToNewpos(Vector3 rightHandTargetPos)
    {
        targetR.position = rightHandTargetPos;
    }
    public void MoveConstraintTargetsToNewPos(Vector3 leftHandTargetPos,Vector3 rightHandTargetPos)
    {
        targetL.position = leftHandTargetPos;
        targetR.position = rightHandTargetPos;

        //
    }
    IEnumerator increaseRigWeight()
    {
        while(rig.weight<1)
        {
            rig.weight += rigIncreaseSpeed*Time.deltaTime;
            yield return null;
        }
        isRigOn = true;
        sorter.swapItems();
    }
    public void RemoveRigWeight()
    {
        StartCoroutine(decreaseRigWeight());
    }
    IEnumerator decreaseRigWeight()
    {
        while (rig.weight > 0)
        {
            rig.weight -= rigIncreaseSpeed * Time.deltaTime;
            yield return null;
        }
        isRigOn = false;
    }
    public Vector3 GetLeftTargetPos()
    {
        return targetL.position;
        
    }
    public Vector3 GetRighttargetPos()
    {
        return targetR.position;
    }
}
