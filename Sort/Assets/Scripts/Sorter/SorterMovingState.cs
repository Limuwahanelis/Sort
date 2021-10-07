using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterMovingState : SorterState
{
    Vector3 targetPos;
    public SorterMovingState(Sorter sorter,Vector3 targetPos):base(sorter)
    {
        sorter.isStandingAtTargetItem = false;
        this.targetPos = targetPos;
        if (Mathf.Abs(Mathf.Abs(sorter.transform.position.x) - Mathf.Abs(targetPos.x)) < 0.05)
        {
            sorter.transform.position = new Vector3(targetPos.x, sorter.transform.position.y, sorter.transform.position.z);
            sorter.isStandingAtTargetItem = true;

            sorter.ChangeState(new SorterIdleState(sorter));
        }
        if (targetPos.x > sorter.transform.position.x)
        {
            sorter.anim.SetBool("Move Right", true);
        }
        else
        {
            sorter.anim.SetBool("Move Left", true);
        }
    }
    public override void Update()
    {
        if(Mathf.Abs( Mathf.Abs(sorter.transform.position.x)-Mathf.Abs(targetPos.x))>0.05 )
        {
           sorter.transform.position= Vector3.MoveTowards(sorter.transform.position, targetPos,Time.deltaTime*sorter.movementSpeed);
        }
        else
        {
            
            sorter.transform.position = new Vector3(targetPos.x, sorter.transform.position.y, sorter.transform.position.z);
            sorter.isStandingAtTargetItem = true;
            
            sorter.ChangeState(new SorterIdleState(sorter));
            sorter.sortingAlgorithm.MoveToNextStep();
            //sorter.StartCoroutine(sorter.WaitSomeTIme(1f, () =>
            //{
            //    sorter.sortingAlgorithm.MoveToNextStep();
            //}));

        }
    }
}
