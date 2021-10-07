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
        if(Mathf.Abs( Mathf.Abs(sorter.transform.position.x)-Mathf.Abs(targetPos.x))>0.05 )
        {
           sorter.transform.position= Vector3.MoveTowards(sorter.transform.position, targetPos,Time.deltaTime*sorter.movementSpeed);
            //sorter.transform.Translate(sorter.transform.right * direction * sorter.movementSpeed * Time.deltaTime);
        }
        else
        {
            
            sorter.transform.position = new Vector3(targetPos.x, sorter.transform.position.y, sorter.transform.position.z);
            sorter.isStandingAtTargetItem = true;
            
            sorter.ChangeState(new SorterIdleState(sorter));
            sorter.StartCoroutine(sorter.WaitSomeTIme(1f, () =>
            {
                sorter.sortingAlgorithm.MoveToNextStep();
            }));
            
            
            Debug.Log("arrived");
            
        }
    }
}
