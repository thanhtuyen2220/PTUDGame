using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamDocChet : MonoBehaviour
{
    Vector2 ViTriChet;
    GameObject Mario;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ViTriChet = transform.localPosition;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Player" && col.contacts[0].normal.y < 0)
        {
            //Destroy(gameObject);
            GameObject HinhNamDocChet = (GameObject)Instantiate(Resources.Load("Prefabs/NamDocChet"));
            HinhNamDocChet.transform.localPosition = ViTriChet;
        }
        //phan nay thua, nen check lai roi bo
        if(col.collider.tag == "NenDat" && col.contacts[0].normal.y < 0)
        {
            GameObject HinhNamDocChet = (GameObject)Instantiate(Resources.Load("Prefabs/NamDocChet"));
            HinhNamDocChet.transform.localPosition = ViTriChet;
        }
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
        }
        
    }
}
