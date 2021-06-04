using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioScript : MonoBehaviour
{
    public static string Level;
    private float VanToc = 7;//Vận tốc của Mario khi di chuyển
    private float VanTocToiDa = 12f;//Vận tốc tối đa khi giữ phím Z - Chạy nhanh
    private float TocDo;//Kiểm tra trạng thái tốc độ của Mario(trong Animator)
    private bool DuoiDat = true;//Kiểm tra xem Mario có đang ở dưới đất hay không(trong Animator)
    private float NhayCao = 490;//Áp dụng khi Mario nhảy cao, giữ phím Space
    private float NhayThap = 5;//Áp dụng khi Mario nhảy thấp, nhấn nhanh và buông phím Space
    private float RoiXuong = 5; //Lực hút rơi xuống của Mario
    private bool ChuyenHuong = false;//Kiểm tra trạng thái chuyển hướng của Mario(trong Animator)
    private bool QuayPhai = true;//Kiểm tra xem Mario có đang quay phải hay không
    private float KTGiuPhim = 0.2f;//Kiểm tra xem có đang giữ chuột trái hay không
    private float TGGiuPhim = 0;//Thời gian giữ chuột trái
    private Rigidbody2D r2d;
    private Animator HoatHoa;
    public int CapDo = 0;//Hiển thị cấp độ của Mario
    public bool BienHinh = false;//Kiểm tra xem Mario có đang biến hình hay không
    private AudioSource AmThanh;
    private Vector2 ViTriChet;//Vi tri luc Mario chet


    private void Awake()
    {

    }
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        HoatHoa = GetComponent<Animator>();
        AmThanh = GetComponent<AudioSource>();
    }
    void Update()
    {
        HoatHoa.SetFloat("TocDo", TocDo);
        HoatHoa.SetBool("DuoiDat", DuoiDat);
        HoatHoa.SetBool("ChuyenHuong", ChuyenHuong);
        NhayLen();
        TangToc();
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
                default: BienHinh = false; break;
            }
        }
        if (gameObject.transform.position.y < -10f)
        {
            MarioDie();
        }
    }
    public void FixedUpdate()
    {
        DiChuyen();
    }
    //Tương tác xuyên vật thể
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "NenDat" || col.gameObject.tag == "MaiRua" || col.gameObject.tag == "KhoiHoiCham")
        {
            DuoiDat = true;
        }
        if (col.gameObject.tag == "Xu")
        {
            TaoAmThanh("Coin");
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "ThanhLua" || col.gameObject.tag == "ThanhLua2" || col.gameObject.tag == "ThanhLua3" || col.gameObject.tag == "ThanhLua4")
        {
            MarioDie();
        }
    }
    //Kiểm tra xem có đang đứng yên hay không khi rơi xuống nền đất
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }
    //Tương tác khi va chạm vật thể
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "KeThu" && collision.contacts[0].normal.y > 0)
        {
            Destroy(collision.gameObject);
            TaoAmThanh("Kick");
        }
    }
    //Làm cho Mario có thể di chuyển trái, phải được
    void DiChuyen()
    {
        float PhimNhanPhaiTrai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc * PhimNhanPhaiTrai, r2d.velocity.y);
        TocDo = Mathf.Abs(VanToc * PhimNhanPhaiTrai);
        if (PhimNhanPhaiTrai > 0 && !QuayPhai) HuongMatMario();
        if (PhimNhanPhaiTrai < 0 && QuayPhai) HuongMatMario();
    }
    //Click chuột trái -> Mario chạy nhanh hơn
    void TangToc()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            TGGiuPhim += Time.deltaTime;
            if (TGGiuPhim < KTGiuPhim)
            {

            }
            else
            {
                VanToc = VanToc * 2.0f;
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
    //Làm cho Mario chuyển hướng khi di chuyển ngược lại
    void HuongMatMario()
    {
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
        //transform.Rotate(0f, 180f, 0f);
        if (TocDo > 1) StartCoroutine(MarioChuyenHuong());
    }
    //Làm cho Mario có thể nhảy và nhảy cao
    void NhayLen()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DuoiDat == true)//Nhấn phím Space -> Mario nhảy
        {
            r2d.AddForce((Vector2.up) * NhayCao);
            TaoAmThanh("Jump");
            DuoiDat = false;
        }
        //Áp dụng lực hút - Mario rơi nhanh hơn
        if (r2d.velocity.y < 0)//Nếu Mario đang ở trên cao và giữ phím Space
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) //Nếu Mario đang ở trên cao và không giữ phím Space
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;
        }
    }
    //Hiệu ứng khi Mario chết
    public void MarioDie()
    {
        ViTriChet = transform.localPosition;
        GameObject MarioChet = (GameObject)Instantiate(Resources.Load("Prefabs/MarioChet"));
        MarioChet.transform.localPosition = ViTriChet;
        Destroy(gameObject);
        Destroy(GameObject.FindWithTag("Music"));
    }
    //Tạo Audio các thao tác trong game
    public void TaoAmThanh(string FileAmThanh)
    {
        AmThanh.PlayOneShot(Resources.Load<AudioClip>("Audio/" + FileAmThanh));
    }
    //Hieu ung Mario chuyen huong
    IEnumerator MarioChuyenHuong()
    {
        ChuyenHuong = true;
        yield return new WaitForSeconds(0.2f);
        ChuyenHuong = false;
    }
    //Nhỏ -> Lớn
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
    //Lớn -> Nhỏ
    IEnumerator MarioThuNho()
    {
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        yield return new WaitForSeconds(DoTre);

    }
}

