using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuaXanhChet : MonoBehaviour
{
    Vector2 ViTriChet;

    // Update is called once per frame
    void Update()
    {
        ViTriChet = transform.localPosition;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Player" && col.contacts[0].normal.y < 0)
        {
            Destroy(gameObject);
            GameObject HinhRuaXanhChet = (GameObject)Instantiate(Resources.Load("Prefabs/RuaXanhChet"));
            HinhRuaXanhChet.transform.localPosition = ViTriChet;
           
        }
    }    
}
