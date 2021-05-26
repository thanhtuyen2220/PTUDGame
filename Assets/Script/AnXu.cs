using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnXu : MonoBehaviour
{
    Vector2 ViTriAnXu;
    GameObject Mario;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ViTriAnXu = transform.localPosition;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Xu")
        {
            Destroy(col.gameObject);
        }
    }

}
