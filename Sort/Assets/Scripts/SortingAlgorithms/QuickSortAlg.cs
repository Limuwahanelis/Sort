using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSortAlg : Sort
{
    enum STEP
    {
        SELECT_PIVOT,
        GO_TO_LOCATION,
        PUSH_PIVOT,
        PICK_PIVOT,
        PICK_ITEM_UP,
        PUT_ITEM_DOWN,
        COMPARE,
        SWAP,
        SORT,
        INCREASE_INDEX,
        SELECT_PARTITION,
        MOVE_UP,
        SELECT_OTHER_PARTITION,

    }
    class qSortData
    {
        public int iteration;
        public int left;
        public int right;
        public int indexJ;
        public bool sortedleft;
        public bool sortedRight;
        public qSortData(int iteration,int left,int right,int indexJ,bool isSortingLeft)
        {
            this.iteration = iteration;
            this.left = left;
            this.right = right;
            this.indexJ = indexJ;
            this.sortedleft = isSortingLeft;
            this.sortedRight = !isSortingLeft;
        }

    }
    List<qSortData> qSorts = new List<qSortData>();
    STEP mainStep;
    STEP subStep;
    ItemToSort curPivot;
    Vector3 newLoc;
    Vector3 curLastItemPos;
    Vector3 tmpPos;
    
    int iteration;
    int curLeft;
    int curRight;
    int indexI;
    int indexJ;
    int itemDownNum=0;

    //int itemsPickedUp = 0;
    //int itemsPutDown = 0;
    public QuickSortAlg(List<ItemToSort> items, Sorter sorter) : base(items, sorter)
    {
        qSorts.Add(new qSortData(0, 0, 0, 0, false));
        iteration = 1;
        indexI = indexJ = 0;
        curLeft = 0;
        curRight = itemsToSort.Count - 1;
        mainStep = STEP.SELECT_PIVOT;
        subStep = STEP.SELECT_PIVOT;
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
                case STEP.SELECT_PIVOT:
                    {

                        if(subStep==STEP.SELECT_PIVOT)
                        {
                            indexI = (curLeft + curRight) / 2;
                            curLastItemPos = itemsToSort[curRight].transform.position;
                            curPivot = itemsToSort[indexI];
                            Debug.Log("New left: " + curLeft);
                            Debug.Log("New Pivot: " + indexI);
                            Debug.Log("New right: " + curRight);
                            goNUM++;
                            subStep = STEP.GO_TO_LOCATION;
                        }
                        if (subStep == STEP.GO_TO_LOCATION)
                        {
                            if (goNUM == 1)
                            {
                                newLoc = new Vector3(curPivot.transform.position.x, sorter.transform.position.y, sorter.transform.position.z);
                                sorter.ChangeState(new SorterMovingState(sorter, newLoc));
                                canPerformNextStep = false;
                                subStep = STEP.PUSH_PIVOT;
                                break;
                            }
                            if (goNUM == 2)
                            {
                                newLoc = new Vector3(itemsToSort[curRight].transform.position.x, sorter.transform.position.y, sorter.transform.position.z);
                                sorter.ChangeState(new SorterMovingState(sorter, newLoc));

                                canPerformNextStep = false;
                                subStep = STEP.PICK_ITEM_UP;
                                break;
                            }
                            if(goNUM==3)
                            {
                                newLoc = new Vector3(curPivot.transform.position.x, sorter.transform.position.y, sorter.transform.position.z);
                                sorter.ChangeState(new SorterMovingState(sorter, newLoc));
                                canPerformNextStep = false;
                                subStep = STEP.PUT_ITEM_DOWN;
                                break;
                            }
                        }
                        if(subStep==STEP.PUSH_PIVOT)
                        {
                            sorter.ChangeState(new SorterPushingState(sorter, curPivot));
                            canPerformNextStep = false;
                            goNUM++;
                            subStep = STEP.GO_TO_LOCATION;
                            break;
                        }
                        if(subStep==STEP.PICK_ITEM_UP)
                        {
                            sorter.ChangeState(new SorterPickingItemUpState(sorter, itemsToSort[curRight]));
                            goNUM++;
                            subStep = STEP.GO_TO_LOCATION;
                            canPerformNextStep = false;
                            break;
                        }
                        if(subStep==STEP.PUT_ITEM_DOWN)
                        {
                            sorter.ChangeState(new SorterPuttingItemDownState(sorter,itemsToSort[curRight]));
                            Swap(indexI, curRight);
                            swaps++;
                            sorter.counter.UpdateCounter(swaps, comparisons);
                            canPerformNextStep = false;
                            mainStep = STEP.COMPARE;
                            subStep = STEP.GO_TO_LOCATION;
                            indexI = indexJ = curLeft;
                            goNUM = 0;
                            break;
                        }
                        break;
                    }
                case STEP.COMPARE:
                    {


                        if(subStep==STEP.COMPARE)
                        {
                            canPerformNextStep = false;
                            sorter.ChangeState(new SorterComparingState(sorter, itemsToSort[indexI], curPivot));
                            comparisons++;
                            sorter.counter.UpdateCounter(swaps, comparisons);
                            if (itemsToSort[indexI].value < curPivot.value)
                            {
                                mainStep = STEP.SWAP;
                                swapSubStep = SWAP_STEP.GO_TO_LOCATION;
                                itemsPickedUp = 1;
                                goNUM = 1;
                            }
                            else
                            {
                                mainStep = STEP.INCREASE_INDEX;
                                subStep = STEP.INCREASE_INDEX;
                            }
                            break;
                        }
                        if (subStep==STEP.GO_TO_LOCATION)
                        {
                            if(goNUM==0)
                            {
                                newLoc = new Vector3(itemsToSort[indexI].transform.position.x, sorter.transform.position.y, sorter.transform.position.z);
                                sorter.ChangeState(new SorterMovingState(sorter, newLoc));
                                canPerformNextStep = false;
                                subStep = STEP.COMPARE;
                                break;
                            }
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
                        if (subStep == STEP.INCREASE_INDEX)
                        {
                            
                            indexI++;
                            if (indexI >= curRight)
                            {
                                subStep = STEP.GO_TO_LOCATION;
                                goNUM = 1;
                                break;
                            }
                            else
                            {
                                mainStep = STEP.COMPARE;
                                subStep = STEP.GO_TO_LOCATION;
                                goNUM = 0;
                                break;
                            }
                        }
                        if(subStep==STEP.GO_TO_LOCATION)
                        {
                            if(goNUM==1)
                            {
                                MoveToNewPos(itemsToSort[indexJ]);
                                subStep = STEP.PICK_ITEM_UP;
                                canPerformNextStep = false;
                                break;
                            }
                            if (goNUM == 2)
                            {
                                MoveToNewPos(curLastItemPos);
                                canPerformNextStep = false;
                                subStep = STEP.PUT_ITEM_DOWN;
                                itemDownNum = 1;
                            }
                            if (goNUM == 3)
                            {
                                MoveToNewPos(curPivot);
                                subStep = STEP.PICK_PIVOT;
                                canPerformNextStep = false;
                            }
                            if (goNUM == 4)
                            {
                                MoveToNewPos(tmpPos);
                                canPerformNextStep = false;
                                itemDownNum = 2;
                                subStep = STEP.PUT_ITEM_DOWN;
                            }
                            break;
                        }
                        if (subStep == STEP.PICK_ITEM_UP)
                        {
                            tmpPos = itemsToSort[indexJ].transform.position;
                            if(itemsToSort[indexJ]==curPivot) sorter.ChangeState(new SorterGrabPushedItemState(sorter, curPivot));
                            else sorter.ChangeState(new SorterPickingItemUpState(sorter, itemsToSort[indexJ]));
                            canPerformNextStep = false;
                            subStep = STEP.GO_TO_LOCATION;
                            goNUM=2;
                            break;

                        }
                        if(subStep==STEP.PUT_ITEM_DOWN)
                        {
                            if(itemDownNum==1)
                            {
                                sorter.ChangeState(new SorterPuttingItemDownState(sorter, itemsToSort[indexJ]));
                                if(itemsToSort[indexJ]==curPivot) // in case
                                {
                                    mainStep = STEP.SELECT_PARTITION;
                                    goNUM = 0;
                                    canPerformNextStep = false;
                                    break;
                                }
                                subStep = STEP.GO_TO_LOCATION;
                                canPerformNextStep = false;
                                goNUM=3;
                            }
                            if(itemDownNum==2)
                            {
                                sorter.ChangeState(new SorterPuttingItemDownState(sorter, curPivot));
                                canPerformNextStep = false;
                                Swap(indexJ, curRight);
                                swaps++;
                                sorter.counter.UpdateCounter(swaps, comparisons);
                                goNUM = 0;
                                mainStep = STEP.SELECT_PARTITION;
                            }
                            break;

                        }
                        if(subStep==STEP.PICK_PIVOT)
                        {
                            sorter.ChangeState(new SorterGrabPushedItemState(sorter, curPivot));
                            canPerformNextStep = false;
                            subStep = STEP.GO_TO_LOCATION;
                            goNUM=4;
                            break;
                        }
                        break;
                    }
                case STEP.SELECT_PARTITION:
                    {
                        int tmpleft = curLeft;
                        int tmpRight = curRight;
                        int tmpJ = indexJ;
                        if (tmpleft < tmpJ - 1)
                        {
                            qSorts.Add(new qSortData(iteration, tmpleft, tmpRight, indexJ, true));
                            makeNewQsortLeft(tmpleft, tmpJ - 1);
                            break;
                        }
                        if (tmpJ + 1 < tmpRight)
                        {
                            qSorts.Add(new qSortData(iteration, tmpleft, tmpRight, indexJ, false));
                            makeNewQsortRight(tmpJ + 1, tmpRight);
                            break;
                        }
                        mainStep = STEP.SELECT_OTHER_PARTITION;
                        break;
                    }
                case STEP.MOVE_UP:
                    {
                        qSorts.RemoveAt(iteration - 1);
                        iteration--;
                        if (iteration <= 1)
                        {
                            MarkItemsAsSorted();
                            canPerformNextStep = false;
                        }
                        else
                        {
                            mainStep = STEP.SELECT_OTHER_PARTITION;
                            canPerformNextStep = true;
                        }


                        break;
                    }
                case STEP.SELECT_OTHER_PARTITION:
                    {
                        qSortData tmp = qSorts[iteration - 1];
                        if(!tmp.sortedRight)
                        {
                            if(tmp.indexJ+1<tmp.right)
                            {
                                curLeft = tmp.indexJ + 1;
                                curRight = tmp.right;
                                tmp.sortedRight = true;
                                mainStep = STEP.SELECT_PIVOT;
                                subStep = STEP.SELECT_PIVOT;
                                goNUM = 0;
                                canPerformNextStep = true;
                                break;
                            }
                            else
                            {
                                tmp.sortedRight = true;
                                
                            }
                        }
                        if(!tmp.sortedleft)
                        {
                            if(tmp.left<tmp.indexJ-1)
                            {
                                curLeft = tmp.left;
                                curRight = tmp.indexJ-1;
                                tmp.sortedleft = true;
                                mainStep = STEP.SELECT_PIVOT;
                                subStep = STEP.SELECT_PIVOT;
                                goNUM = 0;
                                canPerformNextStep = true;
                                break;
                            }
                            else
                            {
                                tmp.sortedleft = true;
                            }
                        }
                        mainStep = STEP.MOVE_UP;
                        canPerformNextStep = true;
                        break;
                    }
            }
        }
    }
    private void makeNewQsortLeft(int left, int right)
    {
        curLeft = left;
        curRight = right;
        iteration++;
        mainStep = STEP.SELECT_PIVOT;
        subStep = STEP.SELECT_PIVOT;
        canPerformNextStep = true;
    }
    private void makeNewQsortRight(int left, int right)
    {
        curLeft = left;
        curRight = right;
        iteration++;
        mainStep = STEP.SELECT_PIVOT;
        subStep = STEP.SELECT_PIVOT;
        canPerformNextStep = true;
    }
    public override void EndSwapStep()
    {
        goNUM = 0;
        itemsPutDown = 0;
        itemsPickedUp = 0;
        indexJ++;
        mainStep = STEP.INCREASE_INDEX;
        subStep = STEP.INCREASE_INDEX;
        goNUM = 0;
    }
}

//void Sortuj_szybko(int lewy, int prawy)
//{
//    int i, j, piwot;

//    i = (lewy + prawy) / 2;
//    piwot = d[i]; d[i] = d[prawy];
//    for (j = i = lewy; i < prawy; i++)
//        if (d[i] < piwot)
//        {
//            swap(d[i], d[j]);
//            j++;
//        }
//    d[prawy] = d[j]; d[j] = piwot;
//    if (lewy < j - 1) Sortuj_szybko(lewy, j - 1);
//    if (j + 1 < prawy) Sortuj_szybko(j + 1, prawy);
//}