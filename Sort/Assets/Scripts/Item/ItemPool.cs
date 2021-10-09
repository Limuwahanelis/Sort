using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [System.Serializable]
    public class pools
    {
        public List<ItemToSort> pool;
    }
    public List<GameObject> itemsToSpawnPrefabs = new List<GameObject>();
    public List<pools> poolOfItems = new List<pools>();

    public ItemToSort GetItem(int itemValue)
    {

        ItemToSort toReturn = poolOfItems[itemValue].pool.Find((x) => !x.isUsed && x.value == itemValue);
        if(toReturn!=null)
        {
            toReturn.isUsed = true;
        }
        if (toReturn == null) toReturn = Instantiate(itemsToSpawnPrefabs.Find((x) => x.GetComponent<ItemToSort>().value == itemValue)).GetComponent<ItemToSort>();
        return toReturn;
    }
}
