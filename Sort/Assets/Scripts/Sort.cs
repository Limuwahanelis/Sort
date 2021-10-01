using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sort : MonoBehaviour
{
    public List<ItemToSort> itemsToSort = new List<ItemToSort>();

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