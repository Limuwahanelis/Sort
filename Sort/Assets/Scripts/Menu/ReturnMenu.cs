using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnMenu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
