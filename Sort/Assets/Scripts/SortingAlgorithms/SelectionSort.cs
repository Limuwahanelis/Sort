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
        PUT_ITEM_DOWN,
        PICK_ITEM_UP
    }
    STEP mainStep;
    int indexI;
    int indexJ;
    //int goNUM = 0;
    //int itemsPickedUp = 0;
    //int itemsPutDown = 0;
    Vector3 tmpPos;
    public SelectionSort(List<ItemToSort> items, Sorter sorter) :base(items,sorter)
    {
        mainStep = STEP.GO_TO_LOCATION;
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
            switch (mainStep)
            {
                case STEP.GO_TO_LOCATION:
                    {
                        if (goNUM == 1)
                        {
                            MoveToNewPos(itemsToSort[indexI]);
                            canPerformNextStep = false;
                            mainStep = STEP.COMPARE;
                        }              
                        break;
                    }
                case STEP.COMPARE:
                    {
                        comparisons++;
                        canPerformNextStep = false;
                        sorter.ChangeState(new SorterComparingState(sorter, itemsToSort[indexI], itemsToSort[indexJ]));
                        if(itemsToSort[indexI].value<itemsToSort[indexJ].value)
                        {
                            tmpPos = itemsToSort[indexI].transform.position;
                            mainStep = STEP.SWAP;
                            swapSubStep = SWAP_STEP.GO_TO_LOCATION;
                            goNUM = 1;
                        }
                        else
                        {
                            mainStep = STEP.INCREASE_INDEX;
                        }
                        break;
                    }
                case STEP.SWAP:
                    {
                        SwapItems(indexI, indexJ);
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
                            MarkItemsAsSorted();
                            break;
                        }
                        mainStep = STEP.GO_TO_LOCATION;
                        goNUM = 1;
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
        mainStep = STEP.INCREASE_INDEX;
    }
}

//for (j = 0; j < N - 1; j++)
//{
//    pmin = j;
//    for (i = j + 1; i < N; i++)
//        if (d[i] < d[pmin]) pmin = i;
//    swap(d[pmin], d[j]);
//}