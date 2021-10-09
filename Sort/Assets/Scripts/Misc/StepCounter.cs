using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StepCounter : MonoBehaviour
{
    public TMP_Text swaps;
    public TMP_Text comparisons;
    public TMP_Text steps;
    // Start is called before the first frame update
    void Start()
    {
        swaps.text = "0";
        comparisons.text = "0";
        steps.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCounter(int swapNum,int comparisonNum)
    {
        swaps.text = swapNum.ToString();
        comparisons.text = comparisonNum.ToString();
        steps.text = (swapNum + comparisonNum).ToString();
    }
}
