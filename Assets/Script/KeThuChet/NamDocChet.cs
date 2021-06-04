using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamDocChet : MonoBehaviour
{
    Vector2 ViTriChet;
    GameObject Mario;
    MarioScript mario;
    // Start is called before the first frame update
    void Start()
    {
        mario = FindObjectOfType<MarioScript>();
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
            GameObject HinhNamDocChet = (GameObject)Instantiate(Resources.Load("Prefabs/NamDocChet"));
            HinhNamDocChet.transform.localPosition = ViTriChet;
        }
        if(col.gameObject.tag=="MaiRua" && (col.contacts[0].normal.x < 0 || col.contacts[0].normal.x > 0))
        {
            Destroy(gameObject);
            PlayerHighScore.PlayerScore += 100;
            mario.TaoAmThanh("Kick");
        }   
    }
}
