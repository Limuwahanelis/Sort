using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemsToSpawnPrefabs = new List<GameObject>();
    public List<GameObject> itemsToSpawnSelectedPrefabs = new List<GameObject>();
    public float distanceBetweenItems;
    public float numberOfSpawnedItems;
    public Sorter sorter;
    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SpawnItems();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Selected();
        }
    }

    public void SpawnItems()
    {
        for(int i=0;i<numberOfSpawnedItems;i++)
        {
            GameObject item = itemsToSpawnPrefabs[Random.Range(0, itemsToSpawnPrefabs.Count)];
            Vector3 pos = new Vector3(-2 -i * distanceBetweenItems, 1.125f, -0.5f);
            GameObject newItem= Instantiate(item, pos, item.transform.rotation);
            sorter.AddItem(newItem.GetComponent<ItemToSort>());
        }
        
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
