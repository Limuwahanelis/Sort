using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterPushingState : SorterState
{
    ItemToSort itemToPush;
    public SorterPushingState(Sorter sorter,ItemToSort itemToPush):base(sorter)
    {
        this.itemToPush = itemToPush;
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
        if (sorter.animFunc.hashandAbove)
        {
            if (itemToPush.transform.position.z < sorter.itemPushedPos.position.z)
            {
                Vector3 newpos = new Vector3(itemToPush.transform.position.x, itemToPush.transform.position.y, itemToPush.transform.position.z + sorter.pushSpeed * Time.deltaTime);
                itemToPush.transform.position = newpos;
            }
            else
            {
                sorter.animFunc.hashandAbove = false;
                sorter.StartCoroutine(sorter.SwapAnimatorWeighs(4, 1));
                sorter.ChangeState(new SorterIdleState(sorter));
                sorter.sortingAlgorithm.MoveToNextStep();
            }
        }
    }
}
