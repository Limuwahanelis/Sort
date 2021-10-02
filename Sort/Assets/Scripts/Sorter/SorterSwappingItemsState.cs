using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterSwappingItemsState : SorterState
{
    Vector3 rightItemNewPos;
    Vector3 leftItemnewPos;

    public SorterSwappingItemsState(Sorter sorter,Vector3 leftHandTarget,Vector3 rightHandTarget):base(sorter)
    {
        leftItemnewPos = leftHandTarget;
        rightItemNewPos = rightHandTarget;
    }
    public override void Update()
    {
        Vector3 RightItemPos = sorter.righthandItem.transform.position;
        Vector3 LeftItemPos = sorter.lefthandItem.transform.position;
        bool movedItems = true;
        if (Mathf.Abs(rightItemNewPos.x)-Mathf.Abs(RightItemPos.x)   > 0.01)
        {
            Vector3 newPosR = new Vector3(RightItemPos.x - sorter.swapSpeed * Time.deltaTime, RightItemPos.y, RightItemPos.z);
            sorter.MoveRighthandItem(newPosR);
            movedItems = false;
        }
        if (Mathf.Abs(LeftItemPos.x) - Mathf.Abs(leftItemnewPos.x) > 0.01)
        {
           Vector3 newposL = new Vector3(LeftItemPos.x + sorter.swapSpeed * Time.deltaTime, leftItemnewPos.y, leftItemnewPos.z);
            sorter.MoveLefthandItem(newposL);
            movedItems = false;
        }
        if (movedItems)
        {
            Debug.Log("swap");
            sorter.sorterRigController.RemoveRigWeight();
            sorter.ChangeState(new SorterIdleState(sorter));
            sorter.StartCoroutine(WaitForRigToOff());
        }

    }
    public IEnumerator WaitForRigToOff()
    {
        while (sorter.sorterRigController.isRigOn) yield return null;
        sorter.swappedItems = true;
    }
}
