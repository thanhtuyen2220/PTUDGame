using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPaused=false;
    public GameObject PauseScreenUI;    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        PauseScreenUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Resume()
    {
        PauseScreenUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void MainMenuGame()
    {
        SceneManager.LoadScene("MenuStartGame");
        Time.timeScale = 1f;
    }
    public void RestartGame()
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
        PlayerHighScore.PlayerScore = 0;
        Time.timeScale = 1f;
    }
}
