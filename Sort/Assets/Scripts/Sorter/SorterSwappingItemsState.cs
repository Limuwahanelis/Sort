using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterSwappingItemsState : SorterState
{
    bool swappedItems = false;
    Vector3 leftItemNewPos;
    Vector3 rightItemNewPos;
    public SorterSwappingItemsState(Sorter sorter,Vector3 leftItemNewPos,Vector3 rightItemNewPos):base(sorter)
    {
        this.leftItemNewPos = leftItemNewPos;
        this.rightItemNewPos = rightItemNewPos;
        sorter.StartCoroutine(sorter.SwapAnimatorWeighs(1, 3));
    }
    public override void Update()
    {
        if(sorter.swappedLayers)
        {
            if(swappedItems)
            {
                sorter.sortingAlgorithm.MoveToNextStep();
                sorter.ChangeState(new SorterIdleState(sorter));
                sorter.swappedLayers = false;
            }
            else
            {
                sorter.anim.SetTrigger("ff");
                sorter.swappedLayers = false;
            }

        }
        if(sorter.isSwappingItems)
        {
            Vector3 RightItemPos = sorter.righthandItem.transform.position;
            Vector3 LeftItemPos = sorter.lefthandItem.transform.position;
            bool movedItems = true;
            if (Mathf.Abs(rightItemNewPos.x) - Mathf.Abs(RightItemPos.x) > 0.01)
            {
                Vector3 newPosR = new Vector3(RightItemPos.x - sorter.swapSpeed * Time.deltaTime, RightItemPos.y, RightItemPos.z);
                sorter.MoveRighthandItem(newPosR);
                movedItems = false;
            }
            if (Mathf.Abs(LeftItemPos.x) - Mathf.Abs(leftItemNewPos.x) > 0.01)
            {
                Vector3 newposL = new Vector3(LeftItemPos.x + sorter.swapSpeed * Time.deltaTime, leftItemNewPos.y, leftItemNewPos.z);
                sorter.MoveLefthandItem(newposL);
                movedItems = false;
            }
            if (movedItems)
            {
                swappedItems = true;
                sorter.isSwappingItems = false;
                Debug.Log("swap");
                sorter.StartCoroutine(sorter.SwapAnimatorWeighs(3, 1));
                

            }

        }
        //if (swappedItems)
        //{
        //    if (sorter.swappedLayers)
        //    {

        //    }
        //}

    }
}
