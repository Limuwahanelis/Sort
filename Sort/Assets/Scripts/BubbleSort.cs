using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : Sort
{
    int innerLoopIndex=0;
    int outerLoopIndex;
    public BubbleSort(List<ItemToSort> items,Sorter sorter):base(items, sorter) 
    {
        outerLoopIndex = itemsToSort.Count - 1;

    }
    public void GoBetweenItems(int itemIndex)
    {
        Vector3 newPos = new Vector3((itemsToSort[itemIndex + 1].transform.position.x + itemsToSort[itemIndex].transform.position.x) / 2, sorter.transform.position.y, sorter.transform.position.z);
        sorter.ChangeState(new SorterMovingState(sorter, newPos));
    }
    public override void PerfromStep()
    {
        if (innerLoopIndex < outerLoopIndex)
        {
            GoBetweenItems(innerLoopIndex);
            sorter.StartCoroutine(WaitForSorterToArrive());
        }
        else
        {
            outerLoopIndex--;
            innerLoopIndex = 0;
            GoBetweenItems(innerLoopIndex);
            sorter.StartCoroutine(WaitForSorterToArrive());
        }
    }

    public void PerformSwapping()
    {

    }

    public bool PerformCheck()
    {
        if (itemsToSort[innerLoopIndex].value > itemsToSort[innerLoopIndex].value) return true;
        return false;
    }
    public override void PerformSorting()
    {

    }

    public IEnumerator WaitForSorterToArrive()
    {
        while (!sorter.isStandingAtTargetItem) yield return null;
        yield return new WaitForSeconds(1f);
        if (itemsToSort[innerLoopIndex + 1].value < itemsToSort[innerLoopIndex].value)
        {
            sorter.swappedItems = false;
            sorter.SetItemsTohands(itemsToSort[innerLoopIndex + 1], itemsToSort[innerLoopIndex]);
            sorter.StartCoroutine(WaitForItemSwap());
        }
        else
        {
            innerLoopIndex++;
            PerfromStep();
        }
        
        
    }
    public IEnumerator WaitForItemSwap()
    {
        while (!sorter.swappedItems) yield return null;
        Swap(innerLoopIndex, innerLoopIndex + 1);
        innerLoopIndex++;
        PerfromStep();
    }
}
        //for (int i = itemsToSort.Count - 1; i > 0; i--)
        //{
        //    for (int j = 0; j < i; j++)
        //    {
        //        if (itemsToSort[j].value > itemsToSort[j + 1].value) Swap(j, j + 1);
        //    }

        //}
