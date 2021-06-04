using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioChet : MonoBehaviour
{
    Vector2 ViTriChet;
    public float TocDoNay = 20.5f;
    public float DoNayCao = 120f;
    public float DelayTime = 5f;
    void Start()
    {
        StartCoroutine(Restart());
    }
    private void Update()
    {
        StartCoroutine(HoatHoaMarioChet());
    }
    IEnumerator HoatHoaMarioChet()
    {
        while(true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + TocDoNay * Time.deltaTime);
            if(transform.localPosition.y >= ViTriChet.y + DoNayCao + 1)
                break;
            yield return null;
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - TocDoNay * Time.deltaTime);
            if(transform.localPosition.y <= -20f)
            {
                Destroy(gameObject);
                break;  
            }
            yield return null;
        }
    }
    IEnumerator Restart()
    {
        yield return new WaitForSecondsRealtime(DelayTime);
        SceneManager.LoadScene("LoseScreen");
    }
}
