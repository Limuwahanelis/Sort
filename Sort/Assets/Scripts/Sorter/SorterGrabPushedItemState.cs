using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterGrabPushedItemState : SorterState
{
    ItemToSort itemToGrab;
    public SorterGrabPushedItemState(Sorter sorter, ItemToSort itemToGrab) : base(sorter)
    {
        sorter.animFunc.hasHandAbovePushedItem = false;
        this.itemToGrab = itemToGrab;
        sorter.StartCoroutine(sorter.SwapAnimatorWeighs(1, 4));
    }
    public override void Update()
    {
        if (sorter.swappedLayers)
        {
            {
                sorter.anim.SetTrigger("Raise Left Hand");
                sorter.swappedLayers = false;
            }

        }
        if(sorter.animFunc.hashandAbove)
        {
            sorter.anim.SetBool("Grab Left Hand", true);
        }
        if (sorter.animFunc.hasHandAbovePushedItem)
        {
            itemToGrab.SetPositionToFollow(sorter.lefthandhandle);
        }
        if(sorter.animFunc.isItemAtHoldPos)
        {
            
            sorter.animFunc.hashandAbove = false;
            sorter.lefthandItem = itemToGrab;
            sorter.ChangeState(new SorterIdleState(sorter));
            sorter.sortingAlgorithm.MoveToNextStep();
        }
    }
}
