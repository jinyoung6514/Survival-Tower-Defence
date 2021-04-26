using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusController : MonoBehaviour
{
    public Slider hpbar;
    float maxHp = 100.0f;
    float curHp = 100.0f;
    PlayerCtrl playerCtrl;
    public Text coin;
    public int _coin = 0;
    AudioSource musicPlayer;
    public AudioClip backgroundMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        MusicPlay();
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        hpbar.value = curHp / maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        ControlHp();
        coin.text = "" + _coin; 
    }

    void ControlHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, playerCtrl._hp / maxHp, Time.deltaTime * 10);
    }

    void MusicPlay()
    {
        musicPlayer.clip = backgroundMusic;
        musicPlayer.loop = true;
        musicPlayer.time = 0;
        musicPlayer.volume = 0.2f;
        musicPlayer.Play();
    }
}
