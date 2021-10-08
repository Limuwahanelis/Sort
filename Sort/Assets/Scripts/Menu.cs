using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Menu : MonoBehaviour
{
    public Sorter sorter;
    public ItemSpawner spawner;
    public GameObject panel;
    public List<Algg> algorithms;
    List<string> _dropdownOptions=new List<string>();
    public TMP_Dropdown algortihmSelectionDropdown;
    public TMP_InputField inputField;
    public IntReference numberOfItemsToSort;
    int _algorithmIndex;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSortingAlgorithm(int sortingAlgorithmIndex)
    {
        _algorithmIndex = sortingAlgorithmIndex;
    }

    public void Confirm()
    {
        panel.SetActive(false);
        spawner.SpawnItems();
        sorter.MakeActive(algorithms[_algorithmIndex]);
        this.gameObject.SetActive(false);
    }

    public void ChangeNumberOfItems(string value)
    {
        if (value == "") return;
        numberOfItemsToSort.value = int.Parse(value);
    }

    public void CheckValue(string value)
    {
        if (value == "") return;
        int parsedvalue = int.Parse(value);
        parsedvalue= Mathf.Clamp(parsedvalue, 2, 30);

        inputField.text = parsedvalue.ToString();
    }
}
