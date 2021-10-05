using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    public float layerSwapSpeed;
    public bool isSwappingItems;
    public bool swappedItems = false;
    public bool swappedLayers;
    public float swapSpeed;
    public ItemToSort lefthandItem;
    public ItemToSort righthandItem;
    public Transform rightHandHandle;
    public Transform lefthandhandle;
    //public SorterRigController sorterRigController;
    public Animator anim;
    public float movementSpeed;
    public bool isStandingAtTargetItem=false;
    public bool hashandAbove = false;
    [SerializeField]
    List<ItemToSort> itemsToSort = new List<ItemToSort>();
    public Sort sortingAlgorithm;
    private SorterState state;
    
    // Start is called before the first frame update
    void Start()
    {
        state = new SorterIdleState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            //sortingAlgorithm = new BubbleSort(itemsToSort, this);
            //sortingAlgorithm = new BubbleSort2(itemsToSort, this);
            //sortingAlgorithm = new BubbleSort2(itemsToSort, this);
            sortingAlgorithm = new QuickSortAlg(itemsToSort, this);
            sortingAlgorithm.PerfromStep();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            state = new SorterSwappingItemsState(this,righthandItem.transform.position,lefthandItem.transform.position);
            //if(anim.GetLayerWeight(3)>=1)
            //{
            //    anim.SetTrigger("ff");
            //}
        }
        state.Update();
        if(sortingAlgorithm!=null)
        {

        sortingAlgorithm.PerfromStep();
        }
    }

    public void AddItem(ItemToSort itemToAdd)
    {
        itemsToSort.Add(itemToAdd);
    }

    public void ChangeState(SorterState newState)
    {
        state = newState;
    }

    public void SetItemsTohands(ItemToSort leftHandItem, ItemToSort rightHandItem)
    {
        this.lefthandItem = leftHandItem;
        this.righthandItem = rightHandItem;
        state = new SorterSwappingItemsState(this, rightHandItem.transform.position, lefthandItem.transform.position);

    }

    public void MoveLefthandItem(Vector3 newPos)
    {
        lefthandItem.transform.position = newPos;
    }
    public void MoveRighthandItem(Vector3 newPos)
    {
        righthandItem.transform.position = newPos;
    }

    public void swapItems()
    {
        //state = new SorterSwappingItemsState(this, righthandItem.transform.position, lefthandItem.transform.position);
    }

    public IEnumerator WaitSomeTIme(float time,System.Action functionToPerform)
    {
        yield return new WaitForSeconds(time);
        functionToPerform();
    }
    public IEnumerator SwapAnimatorWeighs(int layerToReduceWeight,int layerToIncreaseWeight)
    {

        while(anim.GetLayerWeight(layerToIncreaseWeight)<1)
        {
            yield return null;
            float weight1 = anim.GetLayerWeight(layerToIncreaseWeight);
            float weight2 = anim.GetLayerWeight(layerToReduceWeight);
            float value = layerSwapSpeed * Time.deltaTime;
            anim.SetLayerWeight(layerToIncreaseWeight, weight1 + value);
            anim.SetLayerWeight(layerToReduceWeight, weight2 - value);
        }
        anim.SetLayerWeight(layerToIncreaseWeight, 1);
        anim.SetLayerWeight(layerToReduceWeight, 0);
        swappedLayers = true;
    }

    public void Attach()
    {
        //lefthandItem.transform.parent = lefthandhandle;
        //righthandItem.transform.parent = rightHandHandle;
        isSwappingItems = true;
    }
    public void Detach()
    {
        //isSwappingItems = false;
    }

    public void CorrectItemsPos(Vector3 leftItempos,Vector3 rightItemPos)
    {
        lefthandItem.transform.position = leftItempos;
        righthandItem.transform.position = rightItemPos;
    }
}
