using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterPuttingItemDownState : SorterState
{
    ItemToSort item;
    bool isUsingLeftHand;
    public SorterPuttingItemDownState(Sorter sorter,ItemToSort itemToPutDown):base(sorter)
    {
        SelectHandHoldingItem();
        item = itemToPutDown;
        sorter.swappedLayers = false;
        sorter.anim.SetTrigger("Put Item Down");
    }
    public override void Update()
    {
       if(sorter.animFunc.hashandAbove)
        {
            sorter.lefthandItem.transform.position = sorter.inFrontPos.position;
            sorter.lefthandItem.SetPositionTOFollow(null);
            sorter.StartCoroutine(sorter.SwapAnimatorWeighs(4, 1));

        }
       if(sorter.swappedLayers)
        {
            sorter.sortingAlgorithm.MoveToNextStep();
        }
    }

    void SelectHandHoldingItem()
    {
        if(sorter.lefthandItem==item)
        {
            isUsingLeftHand = true;
        }
        else
        {
            isUsingLeftHand = false;
        }
    }
}
