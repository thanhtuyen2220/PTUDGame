using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHighScore : MonoBehaviour
{
    private float TimeLeft = 200;
    public static int PlayerScore = 0;
    public GameObject TimeLeftUI;
    public GameObject PlayerScoreUI;
    public GameObject WorldUI;
    GameObject Mario;
    GameObject KhoiChuaVatPham;
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
        KhoiChuaVatPham = GameObject.FindGameObjectWithTag("KhoiHoiCham");
    }
    void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimeLeftUI.gameObject.GetComponent<Text>().text = ("TIME" + " " + (int)TimeLeft);
        PlayerScoreUI.gameObject.GetComponent<Text>().text= ("MARIO" + " " + PlayerScore);
        WorldUI.gameObject.GetComponent<Text>().text = ("WORLD"+" "+"1-1");
        if (TimeLeft < 0.1f)
        {

            Mario.GetComponent<MarioScript>().MarioDie();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Xu")
        {
            PlayerScore += 100;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KeThu" && collision.contacts[0].normal.y > 0)
        {
            PlayerScore += 100;
        }
        if (collision.gameObject.tag == "Nam")
        {
            PlayerScore += 200;
        }
        if (collision.gameObject.tag == "GioiHan")
        {
            Mario.GetComponent<MarioScript>().TaoAmThanh("StageClear");
            CountScore();
            Destroy(collision.gameObject);
            Destroy(GameObject.FindWithTag("Music"));
        }
        if (collision.gameObject.tag == "KhoiHoiCham"&& collision.contacts[0].normal.y < 0)
        {
            PlayerScore += 100;
        }
    }
    void CountScore()
    {
        PlayerScore = PlayerScore + (int)(TimeLeft * 10);
    }
}

    
