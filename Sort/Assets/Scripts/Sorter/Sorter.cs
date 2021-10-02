using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    public bool swappedItems = false;
    public float swapSpeed;
    public ItemToSort lefthandItem;
    public ItemToSort righthandItem;
    public Transform rightHandHandle;
    public Transform lefthandhandle;
    public SorterRigController sorterRigController;
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
            sortingAlgorithm = new BubbleSort(itemsToSort, this);
            sortingAlgorithm.PerfromStep();
        }
        state.Update();
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
        //sorterRigController.MoveConstraintTargetsToNewPos(leftHandItem.handle.position, rightHandItem.handle.position);
        //sorterRigController.SetFut(leftHandItem.handle.position, rightHandItem.handle.position);
        sorterRigController.SetTargetsForConstraints(leftHandItem.handle, rightHandItem.handle);
        this.lefthandItem = leftHandItem;
        this.righthandItem = rightHandItem;


    }

    public void AttachGameObjectsToHands()
    {
        //lefthandItem.transform.parent = lefthandhandle;
        //righthandItem.transform.parent = rightHandHandle;
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
        state = new SorterSwappingItemsState(this, righthandItem.transform.position, lefthandItem.transform.position);
    }

}
