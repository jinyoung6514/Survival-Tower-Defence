using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerHpbar : MonoBehaviour
{
    public GameObject _hpbar;
    towerAI _towerAI;
    public GameObject _Level;
    bulletCtrl _bulletCtrl;
    statusController _statusController;
    public GameObject _shortageCoin;
    public Text currentDamage;
    public Text currentSpeed;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        _towerAI = gameObject.GetComponent<towerAI>();
    }

    // Update is called once per frame
    void Update()
    {
        _hpbar.GetComponent<Image>().fillAmount = _towerAI._hp / _towerAI.max_hp;
        _statusController = GameObject.Find("UI").GetComponent<statusController>();
        exit();

    }
    void exit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _Level.SetActive(false);
        }
    }

    public void click()
    {
        Debug.Log("실행");
        _Level.SetActive(true);
        currentDamage.text =  "공격력 : " +_towerAI._tower_bullet;
        currentSpeed.text =  "공격속도 강화 : " +count;
    }
    public void Damageupgrade()
    {
        if(_statusController._coin >= 5)
        {
            _Level.SetActive(false);
            _towerAI._tower_bullet +=10.0f;
            _statusController._coin -= 5;
        }else
        {
            StartCoroutine(shortcoin(3.0f));
            _Level.SetActive(false);
        }
        
    }
    public void Speedupgrade()
    {
        if(_statusController._coin >=5)
        {
            _towerAI.attackSpeed -= 0.05f;
            _Level.SetActive(false);
            _statusController._coin -= 5;
            _towerAI.check = true;
            count +=1;
        }
        else
        {
            StartCoroutine(shortcoin(3.0f));
            _Level.SetActive(false);
        }
    }

    IEnumerator shortcoin(float shortcointext)
    {
        _shortageCoin.SetActive(true);
        yield return new WaitForSeconds(shortcointext);
        _shortageCoin.SetActive(false);
    }

    public void exitBtn()
    {
        _Level.SetActive(false);
    }
}
