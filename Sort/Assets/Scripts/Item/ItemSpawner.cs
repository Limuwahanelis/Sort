using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    //public List<GameObject> itemsToSpawnPrefabs = new List<GameObject>();
    [SerializeField]
    private bool _spawnSelected;
    public ItemPool pool;
    public List<GameObject> itemsToSpawnSelectedPrefabs = new List<GameObject>();
    public float distanceBetweenItems;
    public IntReference numberOfSpawnedItems;
    public Sorter sorter;
    public Transform middlePoint;

    private void Start()
    {
        if(_spawnSelected)
        {
            SpawnSelected();
        }
        else
        {

        SpawnItems();
        }
        sorter.MakeActive();
    }

    private void Update()
    {
    }

    public void SpawnItems()
    {
        float startingXPos = middlePoint.position.x -1*((numberOfSpawnedItems.value / 2) * distanceBetweenItems);
        float xPos = startingXPos;
        for (int i=0;i<numberOfSpawnedItems.value;i++)
        {
            //GameObject item = itemsToSpawnPrefabs[Random.Range(0, itemsToSpawnPrefabs.Count)];
            int itemValue =Random.Range(0, 9);
            ItemToSort item = pool.GetItem(itemValue);
            Vector3 pos = new Vector3(xPos, 1.125f, 0.5f);
            item.transform.position=pos;
            //GameObject newItem= Instantiate(item, pos, item.transform.rotation);
            sorter.AddItem(item);
            xPos += distanceBetweenItems;
        }
    }

    public void SpawnSelected()
    {
        float startingXPos = middlePoint.position.x - 1 * ((itemsToSpawnSelectedPrefabs.Count / 2) * distanceBetweenItems);
        float xPos = startingXPos;
        for (int i = 0; i < itemsToSpawnSelectedPrefabs.Count; i++)
        {
            GameObject item = itemsToSpawnSelectedPrefabs[i];
            Vector3 pos = new Vector3(xPos, 1.125f, 0.5f);
            GameObject newItem = Instantiate(item, pos, item.transform.rotation);
            sorter.AddItem(newItem.GetComponent<ItemToSort>());
            xPos += distanceBetweenItems;
        }
    }


}
