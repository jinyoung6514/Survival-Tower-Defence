using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerAI : MonoBehaviour
{
    public float Range;
    public GameObject Target; 
    public GameObject _bullet;
    public Transform _FirePos;
    public float max_hp = 100.0f;
    public float _hp =100.0f;
    const string EnemyTag = "Enemy";
    public float attackSpeed;
    public bool check;
    public float _tower_bullet;
    // Start is called before the first frame update
    void Start()
    {
        check = true;
        attackSpeed = 1.7f;
        _tower_bullet = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            //Debug.Log("실행!");
            InvokeRepeating("updateTarget",0f,attackSpeed);
            Debug.Log(attackSpeed);
            //Debug.Log("실행!");
            check = false;
        }
        if(Target != null)
        {
            transform.LookAt(Target.transform.position);
        }
        if(_hp<=0)
        {
            Destroy(gameObject);
        }
    }

    void updateTarget()
    {
        
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject Enemy in Enemies)
        {
            float DistanceToEnemies = Vector3.Distance(transform.position,Enemy.transform.position);

            if(DistanceToEnemies < shortestDistance)
            {
                shortestDistance = DistanceToEnemies;
                nearestEnemy = Enemy;
            }
        }
        if(nearestEnemy!=null&&shortestDistance <= Range)
        {
            enemyAI _enemyAi = nearestEnemy.GetComponent<enemyAI>();
            Target = nearestEnemy;
            if(!_enemyAi.isDie)             //죽었을 때 공격중지
            {
                StartCoroutine(AttackDelay());
            }
        }
        else{
            Target = null;
        }
    }

    IEnumerator AttackDelay()
    {   
        yield return null;
        Instantiate(_bullet,_FirePos.position,_FirePos.rotation);
        _bullet.GetComponent<bulletCtrl>().damage = _tower_bullet;
        Debug.Log("포탑공격!");
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == EnemyTag)
        {
            _hp -= 20.0f;
        }

    }
}
