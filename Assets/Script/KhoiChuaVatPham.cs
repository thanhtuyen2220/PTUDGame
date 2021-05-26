using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhoiChuaVatPham : MonoBehaviour
{
    private float DoNayCuaKhoi=0.5f;
    private float TocDoNay=4f;
    private bool DuocNay=true;
    private Vector3 ViTriLucDau;
    //Các biến để gán Item (Xu, nấm, sao....)
    public bool ChuaNam=false;
    public bool ChuaXu=false;
    public bool ChuaSao=false;
    //Cho phep so luong xu hien thi
    public int SoLuongXu=-1;


    //Lay cap do cua Mario hien tai
    GameObject Mario;
    private void Awake()
    {
        Mario=GameObject.FindGameObjectWithTag("Player");
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
        if(col.collider.tag == "Player" && col.contacts[0].normal.y>0)
        {
            ViTriLucDau=transform.position;
            KhoiNayLen();
            
        }
    }
    void KhoiNayLen()
    {
        if(DuocNay)
        {
            StartCoroutine(KhoiNay());
            DuocNay=false;
            if(ChuaNam)
            {
                NamVaHoa();
            }
            else if(ChuaXu)
            {
                HienThiXu();
            }
        }
    }
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
    void NamVaHoa(){
        int CapDoHienTai= Mario.GetComponent<MarioScript>().CapDo;
        GameObject Nam=null;
        if(CapDoHienTai==0)
        {
            Nam=(GameObject)Instantiate(Resources.Load("Prefabs/NamAn"));
        }
        else
        {
            Nam=(GameObject)Instantiate(Resources.Load("Prefabs/Hoa"));
        }
        Mario.GetComponent<MarioScript>().TaoAmThanh("ObjectAppear");
        Nam.transform.SetParent(this.transform.parent);
        Nam.transform.localPosition = new Vector2(ViTriLucDau.x,ViTriLucDau.y + 1f);

    }
    void HienThiXu()
    {
        GameObject DongXu = (GameObject)Instantiate(Resources.Load("Prefabs/XuNay"));
        Mario.GetComponent<MarioScript>().TaoAmThanh("Coin");
        DongXu.transform.SetParent(this.transform.parent);
        DongXu.transform.localPosition = new Vector2(ViTriLucDau.x,ViTriLucDau.y + 1f);
        StartCoroutine(XuNayLen(DongXu));
    }

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
