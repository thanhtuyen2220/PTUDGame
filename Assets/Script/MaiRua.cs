using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaiRua : MonoBehaviour
{
    public float VanTocMaiRua = 0;
    public bool MaiRuaDiChuyenTrai = true;
    GameObject Mario;
    public bool Invincible = false;
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
    public void FixedUpdate()
    {
        Vector2 DiChuyen = transform.localPosition;
        if (MaiRuaDiChuyenTrai) DiChuyen.x -= VanTocMaiRua * Time.deltaTime;
        else DiChuyen.x += VanTocMaiRua * Time.deltaTime;
        transform.localPosition = DiChuyen;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag != "Player" && col.contacts[0].normal.x > 0)
        {
            MaiRuaDiChuyenTrai = true;
            QuayMat();
        }
        else if (col.collider.tag != "Player" && col.contacts[0].normal.x < 0)
        {
            MaiRuaDiChuyenTrai = false;
            QuayMat();
        }
        if (col.gameObject.tag == "Player" && (col.contacts[0].normal.x > 0 || col.contacts[0].normal.x < 0))
        {
            if (Mario.GetComponent<MarioScript>().CapDo == 0)
            {
                Mario.GetComponent<MarioScript>().MarioDie();

            }
            else if (Mario.GetComponent<MarioScript>().CapDo == 1)
            {
                Invincible = true;
                Mario.GetComponent<MarioScript>().CapDo -= 1;
                Mario.GetComponent<MarioScript>().BienHinh = true;
                Invoke("ResetInvincible", 2f);
            }
        }
    }
    void QuayMat()
    {
        //Quay mat cua cac vat lai khi doi huong di chuyen
        MaiRuaDiChuyenTrai = !MaiRuaDiChuyenTrai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
    }
    public void ResetInvincible()
    {
        Invincible = false;
    }
}
