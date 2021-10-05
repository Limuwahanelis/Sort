using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort2 : Sort
{
    enum STEP
    {
        GO_TO_LOCATION,
        COMPARE,
        SWAP,
        INCREASE_INDEX
    }
    STEP currentStep;
    int innerLoopIndex = 0;
    int outerLoopIndex;
    public BubbleSort2(List<ItemToSort> items, Sorter sorter) : base(items, sorter)
    {
        outerLoopIndex = itemsToSort.Count - 1;
        canPerformNextStep = true;
        currentStep = STEP.GO_TO_LOCATION;
    }
    public void GoBetweenItems(int itemIndex)
    {
        canPerformNextStep = false;
        Vector3 newPos = new Vector3((itemsToSort[itemIndex + 1].transform.position.x + itemsToSort[itemIndex].transform.position.x) / 2, sorter.transform.position.y, sorter.transform.position.z);
        sorter.ChangeState(new SorterMovingState(sorter, newPos));
    }
    public override void MoveToNextStep()
    {
        currentStep++;
        canPerformNextStep = true;
        if (currentStep > STEP.INCREASE_INDEX) currentStep = STEP.GO_TO_LOCATION;
    }
    public override void PerfromStep()
    {
        if (canPerformNextStep)
        {

            switch (currentStep)
            {
                case STEP.GO_TO_LOCATION:
                    {
                        GoBetweenItems(innerLoopIndex);
                        break;
                    }
                case STEP.COMPARE:
                    {
                        if (PerformCheck())
                        {
                            currentStep++;
                        }
                        else
                        {
                            currentStep = STEP.INCREASE_INDEX;
                        }
                        break;
                    }
                case STEP.SWAP:
                    {
                        SwapItems();
                        break;
                    }
                case STEP.INCREASE_INDEX:
                    {
                        innerLoopIndex++;
                        if (innerLoopIndex >= outerLoopIndex)
                        {
                            innerLoopIndex = 0;
                            outerLoopIndex--;
                        }
                        MoveToNextStep();
                        break;
                    }
            }
        }
    }
    public bool PerformCheck()
    {
        if (itemsToSort[innerLoopIndex].value > itemsToSort[innerLoopIndex+1].value) return true;
        return false;
    }
    public void SwapItems()
    {
        canPerformNextStep = false;
        sorter.SetItemsTohands(itemsToSort[innerLoopIndex + 1], itemsToSort[innerLoopIndex]);
        Swap(innerLoopIndex, innerLoopIndex + 1);
    }
}
