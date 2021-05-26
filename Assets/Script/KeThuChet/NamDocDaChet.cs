using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamDocDaChet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NamDaChetBienMat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator NamDaChetBienMat()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
