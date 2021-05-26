using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnHoa : MonoBehaviour
{
    GameObject Mario;
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            if (Mario.GetComponent<MarioScript>().CapDo == 1)
            {
                Mario.GetComponent<MarioScript>().CapDo += 1;
                Mario.GetComponent<MarioScript>().BienHinh = true;
                Destroy(gameObject);
            }
            else if(Mario.GetComponent<MarioScript>().CapDo == 2)
            {
                Mario.GetComponent<MarioScript>().CapDo += 1;
                Mario.GetComponent<MarioScript>().BienHinh = true;
                Destroy(gameObject);
            }
        }
    }

}
