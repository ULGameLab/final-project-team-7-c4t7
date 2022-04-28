using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSripts : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
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
        
        SceneManager.LoadScene("Level2");
    }
    public void GoToInfo()
    {
        SceneManager.LoadScene("InfoScene");
    }
}
