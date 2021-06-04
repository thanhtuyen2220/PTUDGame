using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeThuScript : MonoBehaviour
{
    GameObject Mario;
    public bool invincible = false;
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            if (collision.collider.tag == "Player" && (collision.contacts[0].normal.x > 0 || collision.contacts[0].normal.x < 0 || collision.contacts[0].normal.y > 0))
            {
                if (Mario.GetComponent<MarioScript>().CapDo > 0)
                {
                    if (Mario.GetComponent<MarioScript>().CapDo == 1)
                    {
                        invincible = true;
                        Mario.GetComponent<MarioScript>().CapDo -= 1;
                        Mario.GetComponent<MarioScript>().BienHinh = true;
                        Invoke("ResetInvulnerability", 2f);
                    }
                }
                else
                {
                    Mario.GetComponent<MarioScript>().MarioDie();
                }
            }
        }
    }
    public void ResetInvulnerability()
    {
        invincible = false;
    }


}
