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
    Vector3 tmpPos;
    STEP currentStep;
    STEP swapSubStep;
    int indexI = 0;
    int indexJ;
    int goNUM = 0;
    int itemsPickedUp = 0;
    int itemsPutDown=0;
    public BubbleSort3(List<ItemToSort> items, Sorter sorter) : base(items, sorter)
    {
        indexJ = itemsToSort.Count - 1;
        canPerformNextStep = true;
        currentStep = STEP.GO_TO_LOCATION;
        goNUM = 1;
    }
    public void GoBetweenItems(int itemIndex)
    {
        canPerformNextStep = false;
        Vector3 newPos = new Vector3((itemsToSort[itemIndex + 1].transform.position.x + itemsToSort[itemIndex].transform.position.x) / 2, sorter.transform.position.y, sorter.transform.position.z);
        sorter.ChangeState(new SorterMovingState(sorter, newPos));
    }
    void MoveToNewPos(ItemToSort item)
    {
        sorter.ChangeState(new SorterMovingState(sorter, new Vector3(item.transform.position.x, sorter.transform.position.y, sorter.transform.position.z)));
    }
    void MoveToNewPos(Vector3 itemPos)
    {
        sorter.ChangeState(new SorterMovingState(sorter, new Vector3(itemPos.x, sorter.transform.position.y, sorter.transform.position.z)));
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
                        if(itemsToSort[indexI].value > itemsToSort[indexI + 1].value)
                        {
                            currentStep = STEP.SWAP;
                            swapSubStep = STEP.PICK_ITEM_UP;
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
                        if(swapSubStep==STEP.GO_TO_LOCATION)
                        {
                            if(goNUM==1)
                            {
                                MoveToNewPos(itemsToSort[indexI+1]);
                                canPerformNextStep = false;
                                swapSubStep = STEP.PICK_ITEM_UP;
                                itemsPickedUp = 2;
                            }
                            if(goNUM==2)
                            {
                                MoveToNewPos(tmpPos);
                                canPerformNextStep = false;
                                swapSubStep = STEP.PUT_ITEM_DOWN;
                                itemsPutDown = 2;
                            }
                            break;
                        }
                        if(swapSubStep==STEP.PICK_ITEM_UP)
                        {
                            if(itemsPickedUp==1)
                            {
                                tmpPos = itemsToSort[indexI].transform.position;
                                sorter.ChangeState(new SorterPickingItemUpState(sorter, itemsToSort[indexI]));
                                canPerformNextStep = false;
                                swapSubStep = STEP.GO_TO_LOCATION;
                                goNUM = 1;
                            }
                            if(itemsPickedUp==2)
                            {
                                PickItemUp(itemsToSort[indexI+1]);
                                swapSubStep = STEP.PUT_ITEM_DOWN;
                                itemsPutDown = 1;
                            }
                            break;

                        }
                        if (swapSubStep == STEP.PUT_ITEM_DOWN)
                        {
                            if (itemsPutDown == 1)
                            {
                                PutItemDown(itemsToSort[indexI]);
                                swapSubStep = STEP.GO_TO_LOCATION;
                                goNUM = 2;
                            }
                            if (itemsPutDown == 2)
                            {
                                PutItemDown(itemsToSort[indexI + 1]);
                                Swap(indexI, indexI + 1);
                                goNUM = 0;
                                itemsPutDown = 0;
                                itemsPickedUp = 0;
                                currentStep = STEP.INCREASE_INDEX;
                            }
                            break;
                        }
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
                        if(indexJ<=0)
                        {
                            canPerformNextStep = false;
                        }
                        break;
                    }
            }
        }
    }
    void PickItemUp(ItemToSort item)
    {
        sorter.ChangeState(new SorterPickingItemUpState(sorter,item));
        canPerformNextStep = false;
    }
    void PutItemDown(ItemToSort item)
    {
        sorter.ChangeState(new SorterPuttingItemDownState(sorter, item));
        canPerformNextStep = false;
    }
    public void SwapItems()
    {
        canPerformNextStep = false;
        sorter.SetItemsTohands(itemsToSort[indexI + 1], itemsToSort[indexI]);
        Swap(indexI, indexI + 1);
    }
}
