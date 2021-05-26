using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioScript : MonoBehaviour
{
    private float VanToc = 7;
    private float VanTocToiDa = 12f;//Vận tốc tối đa khi giữ phím Z - Chạy nhanh
    private float TocDo;
    private bool DuoiDat = true;
    private float NhayCao = 470;
    private float NhayThap = 5;//Áp dụng khi mario nhảy thấp, nhấn nhanh và buông phím Space
    private float RoiXuong = 5; //Lực hút rơi xuống của Mario
    private bool ChuyenHuong = false;
    private bool QuayPhai = true;
    private float KTGiuPhim = 0.2f;//Giu 
    private float TGGiuPhim = 0;
    private Rigidbody2D r2d;
    private Animator HoatHoa;
    GameObject PlayerHighScore;

    //Hiển thị cấp độ và độ lớn của Mario
    public int CapDo = 0;
    public bool BienHinh = false;

    private AudioSource AmThanh;
    //Vi tri luc Mario chet
    private Vector2 ViTriChet;
    //public GameObject Fire;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        HoatHoa = GetComponent<Animator>();
        AmThanh = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HoatHoa.SetFloat("TocDo", TocDo);
        HoatHoa.SetBool("DuoiDat", DuoiDat);
        HoatHoa.SetBool("ChuyenHuong", ChuyenHuong);
        NhayLen();
        BanDanVaTangToc();
        if (BienHinh == true)
        {
            switch (CapDo)
            {
                case 0:
                    {
                        StartCoroutine(MarioThuNho());
                        TaoAmThanh("LevelDown");
                        BienHinh = false;
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(MarioAnNam());
                        TaoAmThanh("Transform");
                        BienHinh = false;
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(MarioAnHoa());
                        TaoAmThanh("Transform");
                        BienHinh = false;
                        break;
                    }
                case 3:
                    {
                        StartCoroutine(MarioAnHoaHaiLan());
                        TaoAmThanh("LevelUp");
                        BienHinh = false;
                        break;
                    }
                default: BienHinh = false; break;
            }
        }
        if (gameObject.transform.position.y < -10f)
        {
            TaoAmThanh("Die");
            Destroy(gameObject);
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void FixedUpdate()
    {
        DiChuyen();

    }
    void DiChuyen()
    {
        float PhimNhanPhaiTrai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc * PhimNhanPhaiTrai, r2d.velocity.y);
        TocDo = Mathf.Abs(VanToc * PhimNhanPhaiTrai);
        if (PhimNhanPhaiTrai > 0 && !QuayPhai) HuongMatMario();
        if (PhimNhanPhaiTrai < 0 && QuayPhai) HuongMatMario();
    }
    void HuongMatMario()
    {
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
        //transform.Rotate(0f, 180f, 0f);
        if (TocDo > 1) StartCoroutine(MarioChuyenHuong());
    }
    void NhayLen()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DuoiDat == true)
        {
            r2d.AddForce((Vector2.up) * NhayCao);
            TaoAmThanh("Jump");
            DuoiDat = false;
        }
        //Áp dụng lực hút - Mario rơi nhanh hơn
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //cai nay thua roi --> bo
        if (col.gameObject.tag == "NenDat")
        {
            DuoiDat = true;
        }
        if (col.gameObject.tag == "Xu")
        {
            TaoAmThanh("Coin");
            Destroy(col.gameObject);
        }
        
        //if (col.gameObject.tag == "Nam")
        //{
        //    CapDo += 1;
        //    BienHinh = true;
        //}
        //if (col.gameObject.tag == "Hoa"&&CapDo==1)
        //{
        //    CapDo += 1;
        //    BienHinh = true;
        //}
        //if(col.gameObject.tag == "Hoa" && CapDo == 2)
        //{
        //    TaoAmThanh("LevelUp");
        //}
        
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }
    IEnumerator MarioChuyenHuong()
    {
        ChuyenHuong = true;
        yield return new WaitForSeconds(0.2f);
        ChuyenHuong = false;
    }
    //Ban dan voi chay nhanh hon
    void BanDanVaTangToc()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            TGGiuPhim += Time.deltaTime;
            if (TGGiuPhim < KTGiuPhim)
            {
                
            }
            else
            {
                VanToc = VanToc * 1.01f;
                if (VanToc > VanTocToiDa)
                {
                    VanToc = VanTocToiDa;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            VanToc = 7f;
            TGGiuPhim = 0;
        }
    }
    //Thay đổi độ lớn của Mario
    IEnumerator MarioAnNam()
    {

        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);

    }
    //an nam -> an hoa
    IEnumerator MarioAnHoa()
    {
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
    }
    //bi thu nho
    IEnumerator MarioThuNho()
    {
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);

    }
    //mario an hoa -> an hoa
    IEnumerator MarioAnHoaHaiLan()
    {
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioAnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
    }
    public void TaoAmThanh(string FileAmThanh)
    {
        AmThanh.PlayOneShot(Resources.Load<AudioClip>("Audio/" + FileAmThanh));
    }
    public void MarioDie()
    {
        ViTriChet = transform.localPosition;
        GameObject MarioChet = (GameObject)Instantiate(Resources.Load("Prefabs/MarioChet"));
        MarioChet.transform.localPosition = ViTriChet;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "KeThu" && collision.contacts[0].normal.y > 0)
        {
            Destroy(collision.gameObject);
            TaoAmThanh("Kick");
            //GameObject HinhNamDocChet = (GameObject)Instantiate(Resources.Load("Prefabs/NamDocChet"));
            //HinhNamDocChet.transform.localPosition = ViTriChet;
        }
    }
}
    
