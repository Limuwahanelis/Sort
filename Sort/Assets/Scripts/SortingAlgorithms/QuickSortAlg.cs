using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSortAlg : Sort
{
    float timeTowait = 0.0f;
    enum STEP
    {
        SELECT_PIVOT,
        COMPARE,
        SWAP,
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
    List<qSortData> qSorts= new List<qSortData>();
    int iteration;
    int curLeft;
    int curRight;
    Vector3 curLastItemPos;
    ItemToSort curPivot;
    STEP currentStep;
    int indexI;
    int indexJ;
    //bool isd = false;
    public QuickSortAlg(List<ItemToSort> items, Sorter sorter) : base(items, sorter)
    {
        qSorts.Add(new qSortData(0, 0, 0, 0, false));
        iteration = 1;
        indexI = indexJ = 0;
        curLeft = 0;
        curRight = itemsToSort.Count - 1;
        currentStep = STEP.SELECT_PIVOT;
        canPerformNextStep = true;
        //PerfromStep();
        //qSort(0, itemsToSort.Count - 1);
    }

    public override void PerfromStep()
    {
        if (canPerformNextStep)
        {
            switch (currentStep)
            {
                case STEP.SELECT_PIVOT:
                    {
                        //
                        indexI = (curLeft + curRight) / 2;
                        curLastItemPos = itemsToSort[curRight].transform.position;
                        curPivot = itemsToSort[indexI];
                        SwapG(indexI, curRight);
                        Vector3 tmpPos = curPivot.transform.position;
                        //ChangePos(indexI, tmpPos);
                        indexJ = indexI = curLeft;
                        curPivot.transform.position = new Vector3(curPivot.transform.position.x, curPivot.transform.position.y, curPivot.transform.position.z + 0.5f);
                        currentStep++;
                        canPerformNextStep = false;
                        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                        break;
                    }

                case STEP.COMPARE:
                    {
                        if (itemsToSort[indexI].value < curPivot.value)
                        {
                            currentStep++;
                        }
                        else
                        {
                            currentStep = STEP.INCREASE_INDEX;
                        }
                        canPerformNextStep = false;
                        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                        break;
                    }
                case STEP.SWAP:
                    {
                        SwapG(indexI, indexJ);
                        indexJ++;
                        currentStep = STEP.INCREASE_INDEX;
                        canPerformNextStep = false;
                        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                        break;
                    }
                case STEP.INCREASE_INDEX:
                    {
                        indexI++;
                        if (indexI >= curRight)
                        {
                            ChangePos(curRight, curLastItemPos);
                            SwapG(indexJ, curRight);
                            currentStep = STEP.SELECT_PARTITION;
                        }
                        else
                        {
                            currentStep = STEP.COMPARE;
                            
                        }
                        canPerformNextStep = false;
                        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                        break;
                    }
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
                        currentStep = STEP.SELECT_OTHER_PARTITION;
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
                            currentStep = STEP.SELECT_OTHER_PARTITION;
                            canPerformNextStep = false;
                            sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
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
                                currentStep = STEP.SELECT_PIVOT;
                                canPerformNextStep = false;
                                sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
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
                                currentStep = STEP.SELECT_PIVOT;
                                canPerformNextStep = false;
                                sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                                break;
                            }
                            else
                            {
                                tmp.sortedleft = true;
                            }
                        }
                        currentStep = STEP.MOVE_UP;
                        canPerformNextStep = false;
                        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
                        break;
                    }
            }
        }
    }
    private void makeNewQsortLeft(int left, int right)
    {
        //qSorts.Add(new qSortData(iteration, left, right, right + 1, true));
        curLeft = left;
        curRight = right;
        iteration++;
        currentStep = STEP.SELECT_PIVOT;
        canPerformNextStep = false;
        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));
        //canPerformNextStep = false;
        //sorter.StartCoroutine(sorter.WaitSomeTIme(1f, () => { canPerformNextStep = true; }));
        //PerfromStep();
    }
    private void makeNewQsortRight(int left, int right)
    {

        //qSorts.Add(new qSortData(iteration, left, right, left - 1, false));
        curLeft = left;
        curRight = right;
        iteration++;
        currentStep = STEP.SELECT_PIVOT;
        canPerformNextStep = false;
        sorter.StartCoroutine(sorter.WaitSomeTIme(timeTowait, () => { canPerformNextStep = true; }));

        //canPerformNextStep = false;
        //sorter.StartCoroutine(sorter.WaitSomeTIme(1f, () => { canPerformNextStep = true; }));
        //PerfromStep();
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