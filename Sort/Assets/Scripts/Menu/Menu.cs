using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public Algorithm selectedAlgorithm;
    public List<Algorithm> algorithms;
    List<string> _dropdownOptions=new List<string>();
    public TMP_Dropdown algortihmSelectionDropdown;
    public TMP_InputField inputField;
    public IntReference numberOfItemsToSort;
    int _algorithmIndex;
    void Start()
    {
        for(int i =0;i<algorithms.Count;i++)
        {
            _dropdownOptions.Add(algorithms[i].name);
        }
        algortihmSelectionDropdown.ClearOptions();
        algortihmSelectionDropdown.AddOptions(_dropdownOptions);

        _algorithmIndex = 0;
        numberOfItemsToSort.value = 2;
    }

    public void SetSortingAlgorithm(int sortingAlgorithmIndex)
    {
        _algorithmIndex = sortingAlgorithmIndex;
    }

    public void Confirm()
    {
        selectedAlgorithm.algorithm = algorithms[_algorithmIndex].algorithm;
        SceneManager.LoadScene(1);
    }
    public void ChangeNumberOfItems(string value)
    {
        if (value == "") return;
        int parsedvalue = int.Parse(value);
        parsedvalue = Mathf.Clamp(parsedvalue, 2, 30);

        inputField.text = parsedvalue.ToString();
        numberOfItemsToSort.value = parsedvalue;
    }

    public void CheckValue(string value)
    {
        if (value == "") return;
        int parsedvalue = int.Parse(value);
        parsedvalue= Mathf.Clamp(parsedvalue, 1, 30);

        inputField.text = parsedvalue.ToString();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
