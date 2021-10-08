using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort3 : Sort
{
    enum STEP
    {
        GO_TO_LOCATION,
        COMPARE,
        SWAP,
        INCREASE_INDEX,
        PICK_ITEM_UP,
        PUT_ITEM_DOWN
    }
    STEP currentStep;
    int indexI = 0;
    int indexJ;
    //int goNUM = 0;
    //int itemsPickedUp = 0;
    //int itemsPutDown = 0;
    public BubbleSort3(List<ItemToSort> items, Sorter sorter) : base(items, sorter)
    {
        indexJ = itemsToSort.Count - 1;
        canPerformNextStep = true;
        currentStep = STEP.GO_TO_LOCATION;
        goNUM = 1;
    }
    public override void MoveToNextStep()
    {
        canPerformNextStep = true;
    }
    public override void PerfromStep()
    {
        if (canPerformNextStep)
        {

            switch (currentStep)
            {
                case STEP.GO_TO_LOCATION:
                    {
                        MoveToNewPos(itemsToSort[indexI]);
                        canPerformNextStep = false;
                        currentStep = STEP.COMPARE;
                        break;
                    }
                case STEP.COMPARE:
                    {
                        if (itemsToSort[indexI].value > itemsToSort[indexI + 1].value)
                        {
                            currentStep = STEP.SWAP;
                            swapSubStep = SWAP_STEP.GO_TO_LOCATION;
                            itemsPickedUp = 1;
                            goNUM = 1;
                        }
                        else
                        {
                            currentStep = STEP.INCREASE_INDEX;
                        }
                        break;
                    }
                case STEP.SWAP:
                    {
                        SwapItems(indexI + 1, indexI);
                        break;
                    }
                case STEP.INCREASE_INDEX:
                    {
                        indexI++;
                        currentStep = STEP.GO_TO_LOCATION;
                        if (indexI >= indexJ)
                        {
                            indexI = 0;
                            indexJ--;
                        }
                        if (indexJ <= 0)
                        {
                            canPerformNextStep = false;
                        }
                        break;
                    }
            }
        }
    }

    public override void EndSwapStep()
    {
        goNUM = 0;
        itemsPutDown = 0;
        itemsPickedUp = 0;
        currentStep = STEP.INCREASE_INDEX;
    }
}
