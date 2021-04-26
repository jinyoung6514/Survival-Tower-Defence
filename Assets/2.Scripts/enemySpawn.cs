using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public Color _color = Color.yellow;
    public float _radius = 1.0f;

    public GameObject _zombiePrefab;
    timeManager tm;
    bool check = true;
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position,_radius); 
    }
    
    void Start()
    {
        tm = GameObject.Find("Time_Text").GetComponent<timeManager>();
        InvokeRepeating("Checktime",0f,1f);
    }
    void Update()
    {
        if((int)tm._Sec % 20 == 0 && check)
        {
            Instantiate(_zombiePrefab,transform.position,transform.rotation);
            check = false;
        }
    }

    void Checktime()
    {
        check = true;
    }
}
