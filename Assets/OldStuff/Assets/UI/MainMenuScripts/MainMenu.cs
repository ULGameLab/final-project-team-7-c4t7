using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("level1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToDemo()
    {
        SceneManager.LoadScene("Demo");
    }
}
