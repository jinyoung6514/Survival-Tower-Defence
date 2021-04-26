using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timeManager : MonoBehaviour
{
    public float _Sec;
    [SerializeField]
    int _Min;
    public Text ClockText;
    PlayerCtrl playerCtrl;
    public GameObject spawnPoint3;
   // public FadeOut _Fade;
    //bool check= true;

    void Start()
    {
        _Min = 5;
        _Sec = 0.0f;
        //_Fade = GameObject.FindObjectOfType<FadeOut>();
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCtrl.Active)
        {   
            _Sec -= Time.deltaTime;
        }
        
        ClockText.text = string.Format("{0:D2}:{1:D2}",_Min,(int)_Sec);

        if((int)_Sec < 0)
        {
            _Sec = 60.0f;
            _Min--;
        }
        if(_Min==0 && (int)_Sec==0)
        {
            
            SceneManager.LoadScene("End");
            
        }
        if(_Min < 1)
        {
            spawnPoint3.SetActive(true);
        }
    }

   
    
}
