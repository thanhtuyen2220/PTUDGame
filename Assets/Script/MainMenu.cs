using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void EasyMode()
    {
        MarioScript.Level = "Easy";
        SceneManager.LoadScene("1-1 (Easy)");
    }
    public void HardMode()
    {
        MarioScript.Level = "Hard";
        SceneManager.LoadScene("1-1 (Hard)");
    }
    public void PlayAgain()
    {
        if (MarioScript.Level == "Easy")
        {
            SceneManager.LoadScene("1-1 (Easy)");
        }
        else if(MarioScript.Level == "Hard")
        {
            SceneManager.LoadScene("1-1 (Hard)");
        }
        PlayerHighScore.PlayerScore = 0;

    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuStartGame");
    }
    public void TryAgain()
    {
        if (MarioScript.Level == "Easy")
        {
            SceneManager.LoadScene("1-1 (Easy)");
        }
        else if (MarioScript.Level == "Hard")
        {
            SceneManager.LoadScene("1-1 (Hard)");
        }
        PlayerHighScore.PlayerScore = 0;
    }

}
