using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    public SorterAnimationFunctions animFunc;
    public Transform itemPushedPos;
    public float layerSwapSpeed;
    public bool isPushingItem=false;
    public bool swappedItems = false;
    public bool swappedLayers;
    public float swapSpeed;
    public float pushSpeed;
    public ItemToSort lefthandItem;
    public ItemToSort righthandItem;
    public Transform rightHandHandle;
    public Transform lefthandhandle;
    public Transform inFrontPos;
    public Animator anim;
    public float movementSpeed;
    public bool isStandingAtTargetItem=false;
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
            sortingAlgorithm = new BubbleSort3(itemsToSort, this);
            //sortingAlgorithm = new QuickSortAlg(itemsToSort, this);
            //sortingAlgorithm = new SelectionSort(itemsToSort, this);
            sortingAlgorithm.PerfromStep();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            state = new SorterSwappingItemsState(this,righthandItem.transform.position,lefthandItem.transform.position);
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

    public IEnumerator WaitSomeTIme(float time,System.Action functionToPerform)
    {
        yield return new WaitForSeconds(time);
        functionToPerform();
    }
    public IEnumerator SwapAnimatorWeighs(int layerToReduceWeight,int layerToIncreaseWeight)
    {
        swappedLayers = false;
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


}
