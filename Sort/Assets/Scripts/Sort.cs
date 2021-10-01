using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sort
{
    protected Sorter sorter;
    public List<ItemToSort> itemsToSort = new List<ItemToSort>();
    public Sort(List<ItemToSort> items,Sorter sorter)
    {
        itemsToSort = items;
        this.sorter = sorter;
    }

    public abstract void PerformSorting();
    public abstract void PerfromStep();
    public void Swap(int firstItemIndex,int secondItemIndex)
    {
        ItemToSort tmp = itemsToSort[firstItemIndex];
        Vector3 posTmp = itemsToSort[firstItemIndex].transform.position;
        itemsToSort[firstItemIndex].transform.position = itemsToSort[secondItemIndex].transform.position;
        itemsToSort[secondItemIndex].transform.position = posTmp;
        itemsToSort[firstItemIndex] = itemsToSort[secondItemIndex];
        itemsToSort[secondItemIndex] = tmp;
    }
}
