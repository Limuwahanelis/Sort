using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterPuttingItemDownState : SorterState
{
    ItemToSort _item;
    bool _isUsingLeftHand;
    bool _isLeftHandHoldingItem;
    bool _isRightHandHoldingItem;
    public SorterPuttingItemDownState(Sorter sorter,ItemToSort itemToPutDown):base(sorter)
    {
        
        _item = itemToPutDown;
        SelectHandToPutItem();
        sorter.swappedLayers = false;
        
    }
    public override void Update()
    {
       if(sorter.animFunc.hashandAbove)
        {
            if(_isUsingLeftHand)
            {
                sorter.lefthandItem.transform.position = sorter.inFrontPos.position;
                sorter.lefthandItem.SetPositionTOFollow(null);
                sorter.lefthandItem = null;
                if(_isRightHandHoldingItem) sorter.StartCoroutine(sorter.LowerAnimationWeight(4));
                else sorter.StartCoroutine(sorter.SwapAnimatorWeighs(4, 1));

            }
            else
            {
                sorter.righthandItem.transform.position = sorter.inFrontPos.position;
                sorter.righthandItem.SetPositionTOFollow(null);
                sorter.righthandItem = null;
                if(_isLeftHandHoldingItem) sorter.StartCoroutine(sorter.LowerAnimationWeight(5));
                else sorter.StartCoroutine(sorter.SwapAnimatorWeighs(5, 1));

            }
            sorter.animFunc.isItemAtHoldPos = false;
            sorter.animFunc.hashandAbove = false;
        }
       if(sorter.swappedLayers)
        {
            sorter.ChangeState(new SorterIdleState(sorter));
            sorter.swappedLayers = false;
            sorter.sortingAlgorithm.MoveToNextStep();
        }
    }

    void SelectHandToPutItem()
    {
        _isLeftHandHoldingItem = sorter.lefthandItem ? true : false;
        _isRightHandHoldingItem = sorter.righthandItem? true : false;
        if (sorter.lefthandItem==_item)
        {
            _isUsingLeftHand = true;
            sorter.anim.SetBool("Grab Left Hand", false);
        }
        else
        {
            _isUsingLeftHand = false;
            sorter.anim.SetBool("Grab Right Hand", false);
        }
    }
}
