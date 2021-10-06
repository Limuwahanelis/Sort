using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterGrabPushedItemState : SorterState
{
    bool _isGrabbing = false;
    ItemToSort itemToGrab;
    public SorterGrabPushedItemState(Sorter sorter, ItemToSort itemToGrab) : base(sorter)
    {
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
        if (sorter.animFunc.hasHandAbovePushedItem)
        {
            itemToGrab.SetPositionTOFollow(sorter.lefthandhandle);
        }
        if(sorter.animFunc.isItemAtHoldPos)
        {
            sorter.animFunc.hashandAbove = false;

            sorter.ChangeState(new SorterIdleState(sorter));
            sorter.sortingAlgorithm.MoveToNextStep();
        }
    }
}
