using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhoiChuaVatPham : MonoBehaviour
{
    private float DoNayCuaKhoi=0.5f;
    private float TocDoNay=4f;
    private bool DuocNay=true;
    private Vector3 ViTriLucDau;
    public bool ChuaNam=false;//Biến gán item Xu
    public bool ChuaXu=false;//Biến gán item Nấm
    public int SoLuongXu=0;//Cho phép số lượng xu hiển thị
    GameObject Mario;
    private void Awake()
    {
        Mario=GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Player" && col.contacts[0].normal.y>0)
        {
            ViTriLucDau=transform.position;
            KhoiNayLen();
        }
    }
    //Làm cho khối ? nảy lên -> hiện ra vật phẩm bên trong
    void KhoiNayLen()
    {
        if(DuocNay)//Nếu khối ? được nảy lên
        {
            StartCoroutine(KhoiNay());
            DuocNay=false;
            if(ChuaNam)//Nếu chứa nấm -> Hiện ra nấm
            {
                Nam();
            }
            else if(ChuaXu)//Nếu chứa xu -> Xu nảy lên
            {
                HienThiXu();
            }
        }
    }
    //Làm cho nấm hiện ra
    void Nam()
    {
        int CapDoHienTai = Mario.GetComponent<MarioScript>().CapDo;
        GameObject Nam = null;
        if (CapDoHienTai == 0)
        {
            Nam = (GameObject)Instantiate(Resources.Load("Prefabs/NamAn"));
        }
        //else
        //{
        //    Nam=(GameObject)Instantiate(Resources.Load("Prefabs/Hoa"));
        //}
        Mario.GetComponent<MarioScript>().TaoAmThanh("ObjectAppear");
        Nam.transform.SetParent(this.transform.parent);
        Nam.transform.localPosition = new Vector2(ViTriLucDau.x, ViTriLucDau.y + 1f);

    }
    //Làm cho xu hiện ra
    void HienThiXu()
    {
        GameObject DongXu = (GameObject)Instantiate(Resources.Load("Prefabs/XuNay"));
        Mario.GetComponent<MarioScript>().TaoAmThanh("Coin");
        DongXu.transform.SetParent(this.transform.parent);
        DongXu.transform.localPosition = new Vector2(ViTriLucDau.x, ViTriLucDau.y + 1f);
        StartCoroutine(XuNayLen(DongXu));
    }
    //Hiệu ứng khối ? nảy lên
    IEnumerator KhoiNay()
    {
        while(true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + TocDoNay * Time.deltaTime);
            if (transform.localPosition.y >= ViTriLucDau.y+DoNayCuaKhoi) break;
            yield return null;
        }
        while(true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - TocDoNay * Time.deltaTime);
            if (transform.localPosition.y <= ViTriLucDau.y) break;
            Destroy(gameObject);
            GameObject KhoiRong=(GameObject)Instantiate(Resources.Load("Prefabs/KhoiTrong"));
            KhoiRong.transform.position=ViTriLucDau;
            yield return null;
        }
    }
    //Hiệu ứng đồng xu nảy lên 
    IEnumerator XuNayLen(GameObject DongXu)
    {
        while(true)
        {
            DongXu.transform.localPosition = new Vector2(DongXu.transform.localPosition.x, DongXu.transform.localPosition.y + TocDoNay * Time.deltaTime);
            if (DongXu.transform.localPosition.y >= ViTriLucDau.y + 10f) break;
            yield return null;
        }
        while(true)
        {
            DongXu.transform.localPosition = new Vector2(DongXu.transform.localPosition.x, DongXu.transform.localPosition.y - TocDoNay * Time.deltaTime);
            if (DongXu.transform.localPosition.y <= ViTriLucDau.y) break;
            Destroy(DongXu.gameObject);
            yield return null;
        }
    }
}
