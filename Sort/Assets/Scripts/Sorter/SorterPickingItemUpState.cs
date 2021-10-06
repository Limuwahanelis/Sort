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
        SelectHandToGrab();
        this.itemToGrab = itemToGrab;
        //sorter.lefthandItem = itemToGrab;
        sorter.animFunc.hashandAbove = false;
    }
    public override void Update()
    {
        if (sorter.swappedLayers)
        {
            {
                if(_isGrabbingWithLeftHand) sorter.anim.SetTrigger("Grab Left Hand");
                else sorter.anim.SetTrigger("Grab Right Hand");
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
            //itemToGrab.transform.SetParent(sorter.transform);
            //itemToGrab.SetPositionTOFollow(null);
            sorter.animFunc.hashandAbove = false;

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