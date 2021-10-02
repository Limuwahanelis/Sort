using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterIdleState : SorterState
{

    public SorterIdleState(Sorter sorter) : base(sorter)
    {
        sorter.anim.SetBool("Move Right", false);
        sorter.anim.SetBool("Move Left", false);

    }

    public override void Update()
    {

    }
}
