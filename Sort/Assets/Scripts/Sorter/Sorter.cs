using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    List<ItemToSort> itemsToSort = new List<ItemToSort>();
    public Sort sortingAlgorithm;
    // Start is called before the first frame update
    void Start()
    {
        sortingAlgorithm = new BubbleSort(itemsToSort);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            sortingAlgorithm.PerformSorting();
        }
    }

    public void AddItem(ItemToSort itemToAdd)
    {
        itemsToSort.Add(itemToAdd);
    }
}
