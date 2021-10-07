using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSort : Sort
{
    enum STEP
    {
        GO_TO_LOCATION,
        COMPARE,
        SWAP,
        INCREASE_INDEX,
        PUT_ITEM_DOWN
    }
    STEP currentStep;
    int indexI;
    int indexJ;
    int goNUM = 0;
    int itemDownNum = 0;
    Vector3 tmpPos;
    public SelectionSort(List<ItemToSort> items, Sorter sorter) :base(items,sorter)
    {
        currentStep = STEP.GO_TO_LOCATION;
        goNUM = 1;
        indexJ = 0;
        indexI = indexJ + 1;
        canPerformNextStep = true;
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
                        if (goNUM == 1)
                        {
                            MoveToNewPos(itemsToSort[indexI]);
                            canPerformNextStep = false;
                            currentStep = STEP.COMPARE;
                        }
                        if(goNUM==2)
                        {
                            MoveToNewPos(itemsToSort[indexJ]);
                            canPerformNextStep = false;
                            currentStep = STEP.SWAP;
                            
                        }
                        if(goNUM==3)
                        {
                            MoveToNewPos(tmpPos);
                            canPerformNextStep = false;
                            currentStep = STEP.PUT_ITEM_DOWN;
                            itemDownNum = 2;
                        }
                        
                        break;
                    }
                case STEP.COMPARE:
                    {
                        if(itemsToSort[indexI].value<itemsToSort[indexJ].value)
                        {
                            tmpPos = itemsToSort[indexI].transform.position;
                            sorter.ChangeState(new SorterPickingItemUpState(sorter, itemsToSort[indexI]));
                            currentStep = STEP.GO_TO_LOCATION;
                            goNUM = 2;
                            canPerformNextStep = false;
                        }
                        else
                        {
                            currentStep = STEP.INCREASE_INDEX;
                        }
                        break;
                    }
                case STEP.SWAP:
                    {
                        sorter.ChangeState(new SorterPickingItemUpState(sorter, itemsToSort[indexJ]));
                        canPerformNextStep = false;
                        currentStep = STEP.PUT_ITEM_DOWN;
                        itemDownNum = 1;
                        break;
                    }
                case STEP.PUT_ITEM_DOWN:
                    {
                        if(itemDownNum==1)
                        {
                        sorter.ChangeState(new SorterPuttingItemDownState(sorter, itemsToSort[indexI]));
                            currentStep = STEP.GO_TO_LOCATION;
                            goNUM = 3;
                        }
                        if(itemDownNum==2)
                        {
                            sorter.ChangeState(new SorterPuttingItemDownState(sorter, itemsToSort[indexJ]));
                            Swap(indexI, indexJ);
                            currentStep = STEP.INCREASE_INDEX;
                            goNUM = 0;
                        }
                        canPerformNextStep = false;
                        break;
                    }
                case STEP.INCREASE_INDEX:
                    {
                        indexI++;
                        if(indexI>=itemsToSort.Count)
                        {
                            indexJ++;
                            indexI = indexJ + 1;
                        }
                        if(indexJ>=itemsToSort.Count-1)
                        {
                            canPerformNextStep = false;
                            break;
                        }
                        currentStep = STEP.GO_TO_LOCATION;
                        goNUM = 1;
                        break;
                    }
            }
        }
    }
    void MoveToNewPos(Vector3 itemPos)
    {
        sorter.ChangeState(new SorterMovingState(sorter, new Vector3(itemPos.x, sorter.transform.position.y, sorter.transform.position.z)));
    }
    void MoveToNewPos(ItemToSort item)
    {
        sorter.ChangeState(new SorterMovingState(sorter, new Vector3(item.transform.position.x, sorter.transform.position.y, sorter.transform.position.z)));
    }
}

//for (j = 0; j < N - 1; j++)
//{
//    pmin = j;
//    for (i = j + 1; i < N; i++)
//        if (d[i] < d[pmin]) pmin = i;
//    swap(d[pmin], d[j]);
//}