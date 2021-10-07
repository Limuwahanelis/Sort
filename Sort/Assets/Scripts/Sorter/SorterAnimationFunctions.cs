using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterAnimationFunctions : MonoBehaviour
{
    public bool hashandAbove = false;
    public bool isItemAtHoldPos = false;
    public bool isSwappingItems;
    public bool hasHandAbovePushedItem;
    public void Attach()
    {
        isSwappingItems = true;
    }

    public void SetHand(int value)
    {
        hashandAbove = (value == 1);
    }

    public void Hold()
    {
        isItemAtHoldPos = true;
    }

    public void HandAbovePushed()
    {
        hasHandAbovePushedItem = true;
    }
}
