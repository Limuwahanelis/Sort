using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterPushingState : SorterState
{
    ItemToSort itemToPush;
    public SorterPushingState(Sorter sorter, ItemToSort itemToPush) : base(sorter)
    {
        sorter.animFunc.hashandAbove = false;
        sorter.animFunc.hasHandAbovePushedItem = false;
        this.itemToPush = itemToPush;
        sorter.StartCoroutine(sorter.SwapAnimatorWeighs(1, 4, () =>
        {
            sorter.anim.SetTrigger("Push Left Hand");
            sorter.anim.SetTrigger("Raise Left Hand");
            sorter.anim.SetLayerWeight(2, 0);
            sorter.swappedLayers = false;
        }));
    }
    public override void Update()
    {
        if (sorter.animFunc.hashandAbove)
        {
            itemToPush.SetPositionTOFollow(sorter.lefthandhandle);
        }
        if(sorter.animFunc.hasHandAbovePushedItem)
        {
            itemToPush.transform.position = sorter.itemPushedPos.position;
            itemToPush.SetPositionTOFollow(null);
        }
        if (sorter.animFunc.isHandback)
        {
            sorter.animFunc.isHandback = false;
            sorter.StartCoroutine(sorter.SwapAnimatorWeighs(4, 1,()=>
            {
                sorter.anim.SetLayerWeight(2, 1);
                sorter.animFunc.hasHandAbovePushedItem = false;
                sorter.animFunc.hashandAbove = false;
                sorter.swappedLayers = false;
                sorter.ChangeState(new SorterIdleState(sorter));
                sorter.sortingAlgorithm.MoveToNextStep();
            }));
            
        }

    }
}
