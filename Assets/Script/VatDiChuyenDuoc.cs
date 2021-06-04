using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VatDiChuyenDuoc : MonoBehaviour
{
    public float VanTocVat;
    public bool DiChuyenTrai = true;

    private void FixedUpdate()
    {
        Vector2 DiChuyen=transform.localPosition;
        if (DiChuyenTrai) DiChuyen.x -= VanTocVat * Time.deltaTime;
        else DiChuyen.x += VanTocVat * Time.deltaTime;
        transform.localPosition = DiChuyen;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag !="Player" && col.contacts[0].normal.x > 0)
        {
            DiChuyenTrai = true;
            QuayMat();
        } 
        else if (col.collider.tag !="Player" && col.contacts[0].normal.x < 0)
        {
            DiChuyenTrai = false;
            QuayMat();
        } 
    }
    void QuayMat()
    {
        //Quay mat cua cac vat lai khi doi huong di chuyen
        DiChuyenTrai = !DiChuyenTrai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
    }
}
