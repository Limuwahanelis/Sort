using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : Sort
{

    public BubbleSort(List<ItemToSort> items):base(items){}
    public override void PerformSorting()
    {
        for (int i = itemsToSort.Count - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (itemsToSort[j].value > itemsToSort[j + 1].value) Swap(j, j + 1);
            }

        }
    }

}
