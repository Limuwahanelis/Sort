using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class Sorter : MonoBehaviour
{

    public SorterRigController sorterRigController;
    public Animator anim;
    public float movementSpeed;
    public bool isStandingAtTargetItem=false;
    List<ItemToSort> itemsToSort = new List<ItemToSort>();
    private Sort sortingAlgorithm;
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


}
