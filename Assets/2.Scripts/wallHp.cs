using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wallHp : MonoBehaviour
{
    public GameObject _hpbar;
    public float max_hp;
    float _hp;
    public GameObject _Level;
    int maxcount;
    public Text currentHp;
    public Text MaxHp;
    statusController _status;
    public GameObject cointext;
    public GameObject maxtext;
    public GameObject recovertext;
    // Start is called before the first frame update
    void Start()
    {
       max_hp = 100.0f;
       _hp = 100.0f;
       _status = GameObject.Find("UI").GetComponent<statusController>();
    }

    // Update is called once per frame
    void Update()
    {
        _hpbar.GetComponent<Image>().fillAmount =_hp / max_hp;
        if(_hp <=0)
        {
            Destroy(gameObject,1f);
        }
        if(_hp>max_hp)
        {
            _hp = max_hp;
        }
        exit();
//        Debug.Log(_hp);
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "Enemy")
        {
            _hp -= 20;
        }
    }

    public void Clickbtn()
    {
        _Level.SetActive(true);
        currentHp.text = "현재 체력 : " + _hp;
        MaxHp.text = "최대 체력 : " + max_hp;
    }
    void exit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _Level.SetActive(false);
        }
    }
    public void ClickRecovery()
    {
        if(_hp<max_hp && _status._coin >=3)
        {
            _hp += 100;
            _status._coin -= 3;
        }else if(_status._coin <3)
        {
            StartCoroutine(coinText(3.0f));
        }
        else if(_hp == max_hp)
        {
            StartCoroutine(recoverText(3.0f));
        }
        _Level.SetActive(false);
    }
    public void ClickMaxhp()
    {
        if(maxcount <= 10 && _status._coin >=3){
            max_hp += 100;
            _hp += 100;
            maxcount ++;
            _status._coin -=3;
        }
        else if(maxcount >10)
        {
            StartCoroutine(maxText(3.0f));
        }
        else if(_status._coin <3)
        {
            StartCoroutine(coinText(3.0f));
        }
        _Level.SetActive(false);
    }
    public void MouseExit()
    {
        _Level.SetActive(false);
    }

    IEnumerator maxText(float _maxtext)
    {
        maxtext.SetActive(true);
        yield return new WaitForSeconds(_maxtext);
        maxtext.SetActive(false);
    }
    IEnumerator coinText(float _cointext)
    {
        cointext.SetActive(true);
        yield return new WaitForSeconds(_cointext);
        cointext.SetActive(false);
    }
    IEnumerator recoverText(float _recovertext)
    {
        recovertext.SetActive(true);
        yield return new WaitForSeconds(_recovertext);
        recovertext.SetActive(false);
    }
}
