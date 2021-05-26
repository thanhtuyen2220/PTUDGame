using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeThuScript : MonoBehaviour
{
    GameObject Mario;
    private void Awake()
    {
        Mario=GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && (collision.contacts[0].normal.x > 0 || collision.contacts[0].normal.x < 0))
        {
            if (Mario.GetComponent<MarioScript>().CapDo > 0)
            {
                switch (Mario.GetComponent<MarioScript>().CapDo)
                {
                    case 1:
                        {
                            Mario.GetComponent<MarioScript>().CapDo -= 1;
                            Mario.GetComponent<MarioScript>().BienHinh = true;
                            break;
                        }
                    case 2:
                        {
                            Mario.GetComponent<MarioScript>().CapDo -= 2;
                            Mario.GetComponent<MarioScript>().BienHinh = true;
                            break;
                        }
                    case 3:
                        {
                            Mario.GetComponent<MarioScript>().CapDo -= 3;
                            Mario.GetComponent<MarioScript>().BienHinh = true;
                            break;
                        }
                    default: Mario.GetComponent<MarioScript>().BienHinh = false; break;
                }
            }
            else
            {
                Mario.GetComponent<MarioScript>().MarioDie();
            }
        }
    }
}
