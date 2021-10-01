using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterMovingState : SorterState
{
    Vector3 targetPos;
    float direction;
    public SorterMovingState(Sorter sorter,Vector3 targetPos):base(sorter)
    {
        sorter.isStandingAtTargetItem = false;
        Debug.Log(targetPos);
        this.targetPos = targetPos;
        if (targetPos.x > sorter.transform.position.x)
        {
            direction = 1f;
            sorter.anim.SetBool("Move Right", true);
        }
        else
        {
            direction = -1f;
            sorter.anim.SetBool("Move Left", true);
        }
    }
    public override void Update()
    {
        if(Mathf.Abs( Mathf.Abs(sorter.transform.position.x)-Mathf.Abs(targetPos.x))>0.01 )
        {
            sorter.transform.Translate(sorter.transform.right * direction * sorter.movementSpeed * Time.deltaTime);
        }
        else
        {
            
            sorter.transform.position = new Vector3(targetPos.x, sorter.transform.position.y, sorter.transform.position.z);
            sorter.isStandingAtTargetItem = true;
            sorter.ChangeState(new SorterIdleState(sorter));
            Debug.Log("arrived");
            
        }
    }
}
