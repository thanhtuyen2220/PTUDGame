using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnNam : MonoBehaviour
{
    GameObject Mario;
    GameObject PlayerHighScore;
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            if (Mario.GetComponent<MarioScript>().CapDo < 1)
            {
                Mario.GetComponent<MarioScript>().CapDo += 1;
                Mario.GetComponent<MarioScript>().BienHinh = true;
                Destroy(gameObject);
            }
        }
    }

}
