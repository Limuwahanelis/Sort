using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ReturnMenu : MonoBehaviour
{
    public TMP_Text speedText;
    public void Exit()
    {
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetSortingSpeed(float value)
    {
        Time.timeScale = value;
        string toPrint=string.Format("{0:0.00}", value);
        speedText.text = toPrint;
    }
    private void Start()
    {
        speedText.text = "1,00";
    }
}
