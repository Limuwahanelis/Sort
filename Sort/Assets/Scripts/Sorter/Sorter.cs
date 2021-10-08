using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    bool _isActive = false;
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
        if (_isActive)
        {
            state.Update();
            if (sortingAlgorithm != null)
            {
                sortingAlgorithm.PerfromStep();
            }
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
    public IEnumerator SwapAnimatorWeighs(int layerToReduceWeight, int layerToIncreaseWeight,System.Action function)
    {
        swappedLayers = false;
        while (anim.GetLayerWeight(layerToIncreaseWeight) < 1)
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
        function();
    }

    public IEnumerator LowerAnimationWeight(int layerToReduceWeight)
    {
        swappedLayers = false;
        while (anim.GetLayerWeight(layerToReduceWeight) > 0)
        {
            yield return null;
            float weight2 = anim.GetLayerWeight(layerToReduceWeight);
            float value = layerSwapSpeed * Time.deltaTime;
            anim.SetLayerWeight(layerToReduceWeight, weight2 - value);
        }
        anim.SetLayerWeight(layerToReduceWeight, 0);
        swappedLayers = true;
    }

    public void MakeActive(Algg algorithm)
    {

        _isActive = true;
        switch(algorithm.algorithm)
        {
            case Enums.Algorithms.BUBBLE_SORT:sortingAlgorithm = new BubbleSort3(itemsToSort, this);break;
            case Enums.Algorithms.SELECTION_SORT: sortingAlgorithm = new SelectionSort(itemsToSort, this); break;
            case Enums.Algorithms.QUICK_SORT: sortingAlgorithm = new QuickSortAlg(itemsToSort, this); break;
        }
        //if(algorithm.algorithm==Enums.Algorithms.BUBBLE_SORT) sortingAlgorithm=new BubbleSort3()

    }


}
