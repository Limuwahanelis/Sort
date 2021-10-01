using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemsToSpawnPrefabs = new List<GameObject>();
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
    }

    public void SpawnItems()
    {
        for(int i=0;i<numberOfSpawnedItems;i++)
        {
            GameObject item = itemsToSpawnPrefabs[Random.Range(0, itemsToSpawnPrefabs.Count)];
            Vector3 pos = new Vector3(0 -i * distanceBetweenItems, 0, 0);
            GameObject newItem= Instantiate(item, pos, item.transform.rotation);
            sorter.AddItem(newItem.GetComponent<ItemToSort>());
        }
        
    }


}
