using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemsToSpawnPrefabs = new List<GameObject>();
    public List<GameObject> itemsToSpawnSelectedPrefabs = new List<GameObject>();
    public float distanceBetweenItems;
    public IntReference numberOfSpawnedItems;
    public Sorter sorter;
    public Transform middlePoint;
    private void Start()
    {
        SpawnItems();
    }

    private void Update()
    {
    }

    public void SpawnItems()
    {
        float startinXPos = middlePoint.position.x + 1*((numberOfSpawnedItems.value / 2) * distanceBetweenItems);
        float xPos = startinXPos;
        for (int i=0;i<numberOfSpawnedItems.value;i++)
        {
            GameObject item = itemsToSpawnPrefabs[Random.Range(0, itemsToSpawnPrefabs.Count)];
            Vector3 pos = new Vector3(xPos, 1.125f, -0.5f);
            GameObject newItem= Instantiate(item, pos, item.transform.rotation);
            sorter.AddItem(newItem.GetComponent<ItemToSort>());
            xPos -= distanceBetweenItems;
        }
        sorter.MakeActive();
    }

    public void Selected()
    {
        for (int i = 0; i < itemsToSpawnSelectedPrefabs.Count; i++)
        {
            GameObject item = itemsToSpawnSelectedPrefabs[i];
            Vector3 pos = new Vector3(-2 - i * distanceBetweenItems, 1.125f, -0.5f);
            GameObject newItem = Instantiate(item, pos, item.transform.rotation);
            sorter.AddItem(newItem.GetComponent<ItemToSort>());
        }
    }


}
