using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SorterState
{
    protected Sorter sorter;

    public abstract void Update();
}
