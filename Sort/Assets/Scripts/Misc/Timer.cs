using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public BoolReference areItemsSorted;
    public GameObject screenToDisplayTime;
    private TMP_Text _text;
    float _time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _text = screenToDisplayTime.GetComponentInChildren<TMP_Text>();
        _text.text = _time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!areItemsSorted.value)
        {
            _time += Time.unscaledDeltaTime;
            //_time += Time.deltaTime;
            _text.text = FormatTime();
        }
    }
    string FormatTime()
    {
        int minutes = (int)_time / 60;
        int seconds = (int)_time %  60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
