using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSortAlg : Sort
{
    float timeTowait = 0.0f;
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
        SELECT_OTHER_PARTITION

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
    Vector3 newLoc;
    List<qSortData> qSorts= new List<qSortData>();
    int iteration;
    int curLeft;
    int curRight;
    Vector3 curLastItemPos;
    Vector3 tmpPos;
    ItemToSort curPivot;
    STEP mainStep;
    STEP subStep;
    int indexI;
    int indexJ;
    int goNUM = 0;
    int itemDownNum=0;
    //bool isd = false;
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
        //PerfromStep();
        //qSort(0, itemsToSort.Count - 1);
    }
    public override void MoveToNextStep()
    {
        canPerformNextStep = true;
        
    }
    void MoveToNewPos(ItemToSort item)
    {
        sorter.ChangeState(new SorterMovingState(sorter,new Vector3( item.transform.position.x, sorter.transform.position.y, sorter.transform.position.z)));
    }
    void MoveToNewPos(Vector3 itemPos)
    {
        sorter.ChangeState(new SorterMovingState(sorter, new Vector3(itemPos.x, sorter.transform.position.y, sorter.transform.position.z)));
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
                            if (itemsToSort[indexI].value < curPivot.value)
                            {
                                subStep = STEP.PICK_ITEM_UP;
                                if (indexI==indexJ)
                                {
                                    mainStep = STEP.INCREASE_INDEX;
                                    subStep = STEP.INCREASE_INDEX;
                                    indexJ++;
                                    break;
                                }
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
                        if(subStep== STEP.PICK_ITEM_UP)
                        {
                            tmpPos = itemsToSort[indexI].transform.position; // store index i position
                            sorter.ChangeState(new SorterPickingItemUpState(sorter, itemsToSort[indexI]));
                            canPerformNextStep = false;
                            mainStep = STEP.SWAP;
                            subStep = STEP.GO_TO_LOCATION;
                            goNUM = 0;
                            break;
                        }
                        // go to indexI
                        // compare to pivot
                        // if < than pivot swap with indexJ 
                        break;
                    }
                case STEP.SWAP:
                    {
                        if(subStep==STEP.PICK_ITEM_UP)
                        {
                            sorter.ChangeState(new SorterPickingItemUpState(sorter, itemsToSort[indexJ]));
                            canPerformNextStep = false;
                            subStep = STEP.SWAP;
                            break;
                        }
                        if(subStep==STEP.GO_TO_LOCATION)
                        {
                            if (goNUM == 0)
                            {
                                MoveToNewPos(itemsToSort[indexJ]);
                                canPerformNextStep = false;
                                subStep = STEP.PICK_ITEM_UP;
                            }
                            if(goNUM==1)
                            {
                                MoveToNewPos(tmpPos); 
                                subStep = STEP.PUT_ITEM_DOWN;
                                canPerformNextStep = false;
                                
                            }
                            break;
                        }
                        if(subStep==STEP.SWAP)
                        {
                            sorter.ChangeState(new SorterPuttingItemDownState(sorter, itemsToSort[indexI]));
                            subStep = STEP.GO_TO_LOCATION;
                            goNUM = 1;
                            canPerformNextStep = false;
                            break;

                        }
                        if(subStep==STEP.PUT_ITEM_DOWN)
                        {
                            sorter.ChangeState(new SorterPuttingItemDownState(sorter, itemsToSort[indexJ]));
                            Swap(indexI, indexJ);
                            indexJ++;
                            canPerformNextStep = false;
                            mainStep = STEP.INCREASE_INDEX;
                            subStep = STEP.INCREASE_INDEX;
                            goNUM = 0;
                            break;
                        }
                        break;
                    }
                //case STEP.COMPARE:
                //    {
                //        if (itemsToSort[indexI].value < curPivot.value)
                //        {
                //            mainStep = STEP.SWAP;
                //        }
                //        else
                //        {
                //            mainStep = STEP.INCREASE_INDEX;
                //        }
                //        //canPerformNextStep = false;
                //        //sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                //        break;
                //    }
                //case STEP.SWAP:
                //    {
                //        //go to indexi grab
                //        //go to indexj grab
                //        //put item i down
                //        //go to prevoious i pos
                //        //put item j down
                //        Swap(indexI, indexJ);
                //        indexJ++;
                //        mainStep = STEP.INCREASE_INDEX;
                //        canPerformNextStep = false;
                //        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                //        break;
                //    }
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
                                //ChangePos(curRight, curLastItemPos);
                                //SwapG(indexJ, curRight);
                                //mainStep = STEP.SELECT_PARTITION;
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
                                //goNUM = 0;
                            }
                            break;
                        }
                        if (subStep == STEP.PICK_ITEM_UP)
                        {
                            tmpPos = itemsToSort[indexJ].transform.position;// store indexJ pos
                            sorter.ChangeState(new SorterPickingItemUpState(sorter, itemsToSort[indexJ]));
                            canPerformNextStep = false;
                            subStep = STEP.GO_TO_LOCATION;
                            goNUM=2;
                            break;

                        }
                        if(subStep==STEP.PUT_ITEM_DOWN)
                        {
                            if(itemDownNum==1)
                            {
                                //tmpPos = itemsToSort[indexJ].transform.position;
                                sorter.ChangeState(new SorterPuttingItemDownState(sorter, itemsToSort[indexJ]));
                                subStep = STEP.GO_TO_LOCATION;
                                canPerformNextStep = false;
                                goNUM=3;
                            }
                            if(itemDownNum==2)
                            {
                                sorter.ChangeState(new SorterPuttingItemDownState(sorter, curPivot));
                                canPerformNextStep = false;
                                Swap(indexJ, curRight);
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
                //case STEP.INCREASE_INDEX:
                //    {
                //        indexI++;
                //        if (indexI >= curRight)
                //        {
                //            ChangePos(curRight, curLastItemPos);
                //            SwapG(indexJ, curRight);
                //            mainStep = STEP.SELECT_PARTITION;
                //        }
                //        else
                //        {
                //            mainStep = STEP.COMPARE;
                            
                //        }
                //        canPerformNextStep = false;
                //        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                //        break;
                //    }
                case STEP.SELECT_PARTITION:
                    {
                        int tmpleft = curLeft;
                        int tmpRight = curRight;
                        int tmpJ = indexJ;
                        //qSorts.Add(new qSortData(iteration, curLeft, curRight,indexJ,true));
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
                        //Move UP
                        break;
                    }
                case STEP.MOVE_UP:
                    {
                        qSorts.RemoveAt(iteration - 1);
                        iteration--;
                        if (iteration <= 1)
                        {
                            Debug.Log("end");
                            canPerformNextStep = false;
                        }
                        else
                        {
                            mainStep = STEP.SELECT_OTHER_PARTITION;
                            canPerformNextStep = false;
                            //sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
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
                                canPerformNextStep = false;
                                //sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
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
                                canPerformNextStep = false;
                                //sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                                break;
                            }
                            else
                            {
                                tmp.sortedleft = true;
                            }
                        }
                        mainStep = STEP.MOVE_UP;
                        canPerformNextStep = false;
                        //sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
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
        canPerformNextStep = false;
        //sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
    }
    private void makeNewQsortRight(int left, int right)
    {
        curLeft = left;
        curRight = right;
        iteration++;
        mainStep = STEP.SELECT_PIVOT;
        subStep = STEP.SELECT_PIVOT;
        canPerformNextStep = false;
        //sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
    }
}
//    public void qSort(int left,int right)
//    {
//        curLeft = left;
//        curRight = right;
//        Debug.Log("Start");
//        Vector3 lastItemPos = itemsToSort[right].transform.position;
//        int i, j;
//        ItemToSort pivot;
//        i = (left + right) / 2;
//        indexI = i;
        
//        pivot = itemsToSort[i]; // push pivot forward 
//        Vector3 tmpPos = pivot.transform.position;
//        Swap(i, right); // place item at right at i - move item at right before pivot
//        ChangePos(i, tmpPos);
//        pivot.transform.position = new Vector3(pivot.transform.position.x, pivot.transform.position.y, pivot.transform.position.z + 0.5f);
//        for (j = i = left; i < right; i++)
//        {
//            if (itemsToSort[i].value < pivot.value)
//            {
//                //Debug.Log("ch");
//                SwapG(i, j); // swap items at i and j
//                j++;
//                //indexJ = j;
//            }
//        }

//        //sorter.StartCoroutine(loopCor(left, right, pivot,lastItemPos));

//        //yield return new WaitForSeconds(1f);
//        ChangePos(right, lastItemPos);
//        //yield return new WaitForSeconds(1f);
//        SwapG(j, right);
//        //yield return new WaitForSeconds(1f);
//        Debug.Log("END");
//        //Debug.Log(j);
//        if (left < j - 1)
//        {
//            qSort(left, j - 1);
//        }
//        if (j + 1 < right)
//        {
//            qSort(j + 1, right);
//        }

//        //if (lewy < j - 1) Sortuj_szybko(lewy, j - 1);
//        //if (j + 1 < prawy) Sortuj_szybko(j + 1, prawy);
//    }
    
//    IEnumerator loopCor( int left, int right,ItemToSort pivot,Vector3 lastItemPos)
//    {
//        int i, j;
//        for (j = i = left; i < right; i++)
//        {
//            if (itemsToSort[i].value < pivot.value)
//            {
//                yield return new WaitForSeconds(1f);
//                //Debug.Log("ch");
//                SwapG(i, j); // swap items at i and j
//                j++;
//            }
//        }
//        //yield return new WaitForSeconds(1f);
//        ChangePos(right, lastItemPos);
//        //yield return new WaitForSeconds(1f);
//        SwapG(j, right);
//        //yield return new WaitForSeconds(1f);
//        Debug.Log("END");
//        //Debug.Log(j);
//        Sel(left, j, i, right);
//        // Debug.Log("end");
//        //d[prawy] = d[j]; d[j] = piwot; // zamien pivot z item na j

//    }

//    IEnumerator wwCor()
//    {
//        yield return new WaitForSeconds(1f);
//    }

//    public void Sel(int left,int j,int i,int right)
//    {
//        if (left < j - 1)
//        {
//            Debug.Log("Left");
//            qSort(left, j - 1);
//        }
//        Debug.Log(j);
//        if (j + 1 < right)
//        {
//            Debug.Log("Right");
//            qSort(j + 1, right);
//        }
//    }
//}

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