using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitButtion()
    {
        Application.Quit();
        Debug.Log("Game Close");
    }
    public void EasyMode()
    {

    }
    public void HardMode()
    {
        SceneManager.LoadScene("1-1 (Hard)");
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("1-1 (Hard)");
        PlayerHighScore.PlayerScore = 0;
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuStartGame");
    }
    public void TryAgain()
    {
        SceneManager.LoadScene("1-1 (Hard)");
        PlayerHighScore.PlayerScore = 0;
    }

}
