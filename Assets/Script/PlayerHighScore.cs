using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHighScore : MonoBehaviour
{
    private float TimeLeft = 120;
    public int PlayerScore = 0;
    public GameObject TimeLeftUI;
    public GameObject PlayerScoreUI;
    void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)TimeLeft);
        PlayerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + PlayerScore);
        if (TimeLeft < 0.1f)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GioiHan")
        {
            CountScore();
        }
        if (collision.gameObject.tag == "Xu")
        {
            PlayerScore += 10;
        }
    }
   //sprite
    void CountScore()
    {
        PlayerScore = PlayerScore + (int)(TimeLeft * 10);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Nam" || (collision.gameObject.tag == "KeThu" && collision.contacts[0].normal.y > 0))
        {
            PlayerScore += 100;
        }
        if(collision.gameObject.tag == "Hoa")
        {
            PlayerScore += 200;
        }
        
    }

    
}
