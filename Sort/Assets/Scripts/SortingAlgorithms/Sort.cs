using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sort
{
    public bool canPerformNextStep;
    protected Sorter sorter;
    public List<ItemToSort> itemsToSort = new List<ItemToSort>();
    public Sort(List<ItemToSort> items,Sorter sorter)
    {
        itemsToSort = items;
        this.sorter = sorter;
    }
    public virtual void MoveToNextStep() { }
    public abstract void PerfromStep();
    public void Swap(int firstItemIndex,int secondItemIndex)
    {
        ItemToSort tmp = itemsToSort[firstItemIndex];
        //Vector3 posTmp = itemsToSort[firstItemIndex].transform.position;
        //itemsToSort[firstItemIndex].transform.position = itemsToSort[secondItemIndex].transform.position;
        //itemsToSort[secondItemIndex].transform.position = posTmp;
        itemsToSort[firstItemIndex] = itemsToSort[secondItemIndex];
        itemsToSort[secondItemIndex] = tmp;
    }
    public void SwapG(int firstItemIndex, int secondItemIndex)
    {
        ItemToSort tmp = itemsToSort[firstItemIndex];
        Vector3 posTmp = itemsToSort[firstItemIndex].transform.position;
        itemsToSort[firstItemIndex].transform.position = itemsToSort[secondItemIndex].transform.position;
        itemsToSort[secondItemIndex].transform.position = posTmp;
        itemsToSort[firstItemIndex] = itemsToSort[secondItemIndex];
        itemsToSort[secondItemIndex] = tmp;
    }
    public void ChangePos(int firstItemIndex,Vector3 newPos)
    {
        itemsToSort[firstItemIndex].transform.position=newPos;
    }
}
