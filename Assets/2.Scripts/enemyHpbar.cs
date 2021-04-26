using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHpbar : MonoBehaviour
{
    public GameObject _hpbar;
    enemyAI _enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        _enemyAI = gameObject.GetComponent<enemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        _hpbar.GetComponent<Image>().fillAmount = _enemyAI._hp / _enemyAI.max_hp;
    }
}
