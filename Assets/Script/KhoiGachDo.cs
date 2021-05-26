using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KhoiGachDo : MonoBehaviour
{
    private float DoNayCuaKhoi = 0.5f;
    private float TocDoNay = 4f;
    private bool DuocNay = true;
    private Vector3 ViTriLucDau;
    //Các biến để gán Item (Xu, nấm, sao....)
    


    //Lay cap do cua Mario hien tai
    GameObject Mario;
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player" && col.contacts[0].normal.y > 0)
        {
            ViTriLucDau = transform.position;
            KhoiNayLen();
        }
    }
    void KhoiNayLen()
    {
        if (DuocNay)
        {
            StartCoroutine(KhoiNay());
            DuocNay = false;
        }
    }
    IEnumerator KhoiNay()
    {
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + TocDoNay * Time.deltaTime);
            if (transform.localPosition.y >= ViTriLucDau.y + DoNayCuaKhoi) break;
            yield return null;
        }
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - TocDoNay * Time.deltaTime);
            if (transform.localPosition.y <= ViTriLucDau.y) break;
            Destroy(gameObject);
            GameObject KhoiRong = (GameObject)Instantiate(Resources.Load("Prefabs/KhoiGachDo"));
            KhoiRong.transform.position = ViTriLucDau;
            yield return null;
        }
    }
}
