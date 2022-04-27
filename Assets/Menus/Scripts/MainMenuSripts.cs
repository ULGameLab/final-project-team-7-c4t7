using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSripts : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(3);
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
        SceneManager.LoadScene("2SceneTest");
    }
}
