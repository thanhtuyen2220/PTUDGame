using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public GameObject FinalScoreUI;
    void Start()
    {
        FinalScoreUI.gameObject.GetComponent<Text>().text = ("SCORE: "+PlayerHighScore.PlayerScore.ToString());
    }
}
