using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemyAI : MonoBehaviour
{
    Transform playerTr;
    public float attackDist = 1.5f;
    Animator anim;
    public bool isDie = false;
    public float _hp = 100.0f;
    public float max_hp = 100.0f;
    [SerializeField]
    float DestroyTime = 3.0f;
    const string bulletTag = "BULLET";
    NavMeshAgent agent;
    [SerializeField]
    float speed = 5.0f;
    public float damage = 20.0f;
    PlayerCtrl playerCtrl;
    statusController _status;
    bool check = true;   //중복방지
    //GameObject _trace =null;
    float dist;
    //bool traceplayer = true;
    public GameObject Target; 
    public float Range;

    void Start()
    {
        check = true;
        playerTr = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>(); 
        agent.autoBraking = false;  //목적지에 가까워지면 속도를 줄임
        //agent.updateRotation = false;    //자동으로 회전하는 기능
        agent.speed = speed;
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        _status = GameObject.Find("UI").GetComponent<statusController>();
        // if(!playerCtrl.page2)
        // {
        //     agent.destination = playerTr.position;
        // }
    }

    // Update is called once per frame
    void Update()
    {   if(playerCtrl.Active)
        {
            enemyCtrl();
        }
        if(isDie && check)
        {
            anim.SetTrigger("isDie");
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            agent.isStopped=true;
            Destroy(gameObject, DestroyTime);
            int _randomcoin = Random.Range(1,4);
            _status._coin += _randomcoin;
            check = false;
        }
    }

    void enemyCtrl()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Player");
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
            Target = nearestEnemy;
            agent.destination = Target.transform.position;
            if(!isDie)
            {
                transform.LookAt(Target.transform.position);
            }
            else{
                agent.isStopped = true;
            }
            dist = Vector3.Distance(gameObject.transform.position, Target.transform.position);
            if(dist <= attackDist)
            {   
                anim.SetBool("isAttack",true);
                agent.isStopped=true;
            }
            else
            {
                anim.SetBool("isAttack",false);
                agent.isStopped=false;
            }
            
        }
        else{
            Target = null;
        }
        // Collider[] colls = Physics.OverlapSphere(transform.position, 1000.0f);
        // for(int i =0;i<colls.Length;i++)
        // {
        //     if(colls[i].tag == "Tower" || colls[i].tag == "Player")
        //     {
        //         if(traceplayer)
        //         {
        //             _trace =colls[i].gameObject;
        //             traceplayer = false;
        //         }
        //         agent.destination = _trace.transform.position;
        //         if(_trace ==null)
        //         {
        //             traceplayer = true;
        //         }
        //         Debug.Log(_trace);
        //         if(!isDie)
        //         {
        //             transform.LookAt(_trace.transform.position);
        //         }
        //     }
        // }
        // dist = Vector3.Distance(gameObject.transform.position, _trace.transform.position);
        // if(dist <= attackDist)
        // {
        //     anim.SetBool("isAttack",true);
        //     agent.isStopped=true;
        // }
        // else
        // {
        //     anim.SetBool("isAttack",false);
        //     agent.isStopped=false;
        // }
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == bulletTag)
        {
            //총알 삭제 
            Destroy(coll.gameObject);

            //생명게이지 차감
            _hp -= coll.gameObject.GetComponent<bulletCtrl>().damage;

            if(_hp <= 0.0f)
            {
                isDie = true;
            }
        }
    }

   
}
