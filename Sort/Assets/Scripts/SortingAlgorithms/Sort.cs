using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sort
{
    protected enum SWAP_STEP
    {
        GO_TO_LOCATION,
        PICK_ITEM_UP,
        PUT_ITEM_DOWN
    }
    public List<ItemToSort> itemsToSort = new List<ItemToSort>();
    public bool canPerformNextStep;
    protected Sorter sorter;
    protected SWAP_STEP swapSubStep;
    Vector3 tmpPos;
    protected int goNUM = 0;
    protected int itemsPickedUp = 0;
    protected int itemsPutDown = 0;
    public Sort(List<ItemToSort> items,Sorter sorter)
    {
        itemsToSort = items;
        this.sorter = sorter;
    }

    public void Swap(int firstItemIndex,int secondItemIndex)
    {
        ItemToSort tmp = itemsToSort[firstItemIndex];
        itemsToSort[firstItemIndex] = itemsToSort[secondItemIndex];
        itemsToSort[secondItemIndex] = tmp;
    }
    public void ChangePos(int firstItemIndex,Vector3 newPos)
    {
        itemsToSort[firstItemIndex].transform.position=newPos;
    }
    protected void SwapItems(int item1Index, int item2Index)
    {

        if (swapSubStep == SWAP_STEP.GO_TO_LOCATION)
        {
            if (goNUM == 1)
            {
                MoveToNewPos(itemsToSort[item1Index]);
                canPerformNextStep = false;
                swapSubStep = SWAP_STEP.PICK_ITEM_UP;
                itemsPickedUp = 1;
            }
            if (goNUM == 2)
            {
                MoveToNewPos(itemsToSort[item2Index]);
                canPerformNextStep = false;
                swapSubStep = SWAP_STEP.PICK_ITEM_UP;
                itemsPickedUp = 2;
            }
            if (goNUM == 3)
            {
                MoveToNewPos(tmpPos);
                canPerformNextStep = false;
                swapSubStep = SWAP_STEP.PUT_ITEM_DOWN;
                itemsPutDown = 2;
            }
            return;
        }
        if (swapSubStep == SWAP_STEP.PICK_ITEM_UP)
        {
            if (itemsPickedUp == 1)
            {
                tmpPos = itemsToSort[item1Index].transform.position;
                PickItemUp(itemsToSort[item1Index]);
                swapSubStep = SWAP_STEP.GO_TO_LOCATION;
                goNUM = 2;
            }
            if (itemsPickedUp == 2)
            {
                PickItemUp(itemsToSort[item2Index]);
                swapSubStep = SWAP_STEP.PUT_ITEM_DOWN;
                itemsPutDown = 1;
            }
            return;

        }
        if (swapSubStep == SWAP_STEP.PUT_ITEM_DOWN)
        {
            if (itemsPutDown == 1)
            {
                PutItemDown(itemsToSort[item1Index]);
                swapSubStep = SWAP_STEP.GO_TO_LOCATION;
                goNUM = 3;
            }
            if (itemsPutDown == 2)
            {
                PutItemDown(itemsToSort[item2Index]);
                Swap(item2Index, item1Index);
                EndSwapStep();
            }
            return;
        }
        return;
    }
    public abstract void EndSwapStep();
    
    public virtual void MoveToNextStep() { }
    public abstract void PerfromStep();
    protected void MoveToNewPos(ItemToSort item)
    {
        sorter.ChangeState(new SorterMovingState(sorter, new Vector3(item.transform.position.x, sorter.transform.position.y, sorter.transform.position.z)));
    }
    protected void MoveToNewPos(Vector3 itemPos)
    {
        sorter.ChangeState(new SorterMovingState(sorter, new Vector3(itemPos.x, sorter.transform.position.y, sorter.transform.position.z)));
    }
    protected void PickItemUp(ItemToSort item)
    {
        sorter.ChangeState(new SorterPickingItemUpState(sorter, item));
        canPerformNextStep = false;
    }
    protected void PutItemDown(ItemToSort item)
    {
        sorter.ChangeState(new SorterPuttingItemDownState(sorter, item));
        canPerformNextStep = false;
    }
}
