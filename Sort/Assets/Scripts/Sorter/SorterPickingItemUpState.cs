using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterPickingItemUpState : SorterState
{
    bool _isGrabbingWithLeftHand;
    bool _isGrabbing = false;
    ItemToSort itemToGrab;
    public SorterPickingItemUpState(Sorter sorter,ItemToSort itemToGrab):base(sorter)
    {
        
        this.itemToGrab = itemToGrab;
        SelectHandToGrab();
        sorter.animFunc.hashandAbove = false;
    }
    public override void Update()
    {
        if (sorter.swappedLayers)
        {
            {
                if(_isGrabbingWithLeftHand) sorter.anim.SetBool("Grab Left Hand",true);
                else sorter.anim.SetBool("Grab Right Hand",true);
                sorter.swappedLayers = false;
            }
        }
        if(sorter.animFunc.hashandAbove)
        {
            if(!_isGrabbing)
            {
                _isGrabbing = true;
                sorter.animFunc.hashandAbove = false;
            }
        }
        if(_isGrabbing)
        {
            _isGrabbing = false;
            if (_isGrabbingWithLeftHand) itemToGrab.SetPositionTOFollow(sorter.lefthandhandle);
            else itemToGrab.SetPositionTOFollow(sorter.rightHandHandle);

        }
        if(sorter.animFunc.isItemAtHoldPos)
        {
            sorter.animFunc.hashandAbove = false;
            sorter.animFunc.isItemAtHoldPos = false;
            sorter.ChangeState(new SorterIdleState(sorter));
            sorter.sortingAlgorithm.MoveToNextStep();
        }
    }

    void SelectHandToGrab()
    {
        if(sorter.lefthandItem==null)
        {
            sorter.lefthandItem = itemToGrab;
            _isGrabbingWithLeftHand = true;
            sorter.StartCoroutine(sorter.SwapAnimatorWeighs(1, 4));
        }
        else
        {
            sorter.righthandItem = itemToGrab;
            _isGrabbingWithLeftHand = false;
            sorter.StartCoroutine(sorter.SwapAnimatorWeighs(1, 5));
        }
    }
}
