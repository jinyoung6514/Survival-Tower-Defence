using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;
    public GameObject Hide_Time; 
    PlayerCtrl playerCtrl;
    void Start()
    {
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade()
    {
        StartCoroutine(FadeFlow(0.0f));
    }
    IEnumerator FadeFlow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(!playerCtrl.page2)
        {
            Hide_Time.SetActive(false);
        }
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime /F_time;
            alpha.a = Mathf.Lerp(0,1,time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;
        yield return new WaitForSeconds(1.0f);
        while(alpha.a > 0f)
        {
            time += Time.deltaTime /F_time;
            alpha.a = Mathf.Lerp(1,0,time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);

        yield return null;
    }
}
