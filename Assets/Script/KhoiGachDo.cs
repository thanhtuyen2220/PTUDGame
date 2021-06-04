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

    private ParticleSystem particle;
    private SpriteRenderer sr;
    private BoxCollider2D b2d;
    //Lay cap do cua Mario hien tai
    GameObject Mario;
    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        b2d = GetComponent<BoxCollider2D>();
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
        if (col.collider.tag == "Player" && col.contacts[0].normal.y > 0 && Mario.GetComponent<MarioScript>().CapDo == 0)
        {
            ViTriLucDau = transform.position;
            Mario.GetComponent<MarioScript>().TaoAmThanh("Bump");
            KhoiNayLen();

        }
        if(col.collider.tag == "Player" && col.contacts[0].normal.y > 0 && Mario.GetComponent<MarioScript>().CapDo > 0)
        {
            ViTriLucDau = transform.position;
            StartCoroutine(Break());
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
    IEnumerator Break()
    {
        particle.Play();
        Mario.GetComponent<MarioScript>().TaoAmThanh("BreakBlock");
        sr.enabled = false;
        b2d.enabled = false;
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
