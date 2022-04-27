using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSripts : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("InfoScene");
    }
    public void GoToInstruction()
    {
        SceneManager.LoadScene("Instructions");
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
    public void GoToInfo()
    {
        SceneManager.LoadScene("InfoScene");
    }
}
