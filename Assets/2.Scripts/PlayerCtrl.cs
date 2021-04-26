using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip WalkF;
}

public class PlayerCtrl : MonoBehaviour
{
    public float speed=5;
    float h;
    float v;
    Vector3 moveDir;
    public PlayerAnim playerAnim;
    public Animation anim;
    public GameObject _bullet;
    public Transform firePos;
    public float _hp = 100.0f;
    const string EnemyTag = "Enemy";
    const string NextPage = "NextPage";
    public bool Active = true;         //정지 상태만들기
    public FadeOut _Fade;
    towerManager _towerManager;
    [HideInInspector]
    public bool page2=false;
    public GameObject page1SpawnPoint;
    public GameObject page2SpawnPoint;
    
    void Start()
    {
       anim = GetComponent<Animation>();
       anim.clip = playerAnim.idle;
       anim.Play();
       _Fade = GameObject.FindObjectOfType<FadeOut>();
       _towerManager = GameObject.Find("UI").GetComponent<towerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Active)
        {
            PlayerMove();
            LookatMouse();
            if(Input.GetMouseButtonDown(0) && !_towerManager.isPreviewActivated)
            {
                Fire();
            }
        }
    }

    void PlayerMove()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(h,0,v).normalized;

        if(h>=0.1f || h<=-0.1f || v>=0.1f || v<=-0.1f)
        {
            anim.CrossFade(playerAnim.WalkF.name,0.1f);
        }
        else{
            anim.CrossFade(playerAnim.idle.name,0.1f);
        }
    
        transform.position += moveDir * speed * Time.deltaTime;
       
    }

    void LookatMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane GroupPlane = new Plane(Vector3.up,Vector3.zero);

        float rayLength;

        if(GroupPlane.Raycast(cameraRay,out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointTolook.x,transform.position.y,pointTolook.z));
        }
    }

    void Fire()
    {
        Instantiate(_bullet,firePos.position,firePos.rotation);
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == EnemyTag)
        {
            _hp -= coll.gameObject.GetComponent<enemyAI>().damage;
        }
        if(coll.collider.tag == NextPage)
        {
            page2 = true;
            _Fade.Fade();
            page1SpawnPoint.SetActive(false);
            StartCoroutine(Teleport(1.5f));
        }
        if(_hp <= 0)
        {
            _Fade.Fade();
            StartCoroutine(Ending(1.0f));
            Active = false;
        }
    }

    IEnumerator Ending(float endTime)
    {
        yield return new WaitForSeconds(endTime);
        SceneManager.LoadScene("End");
    }
    IEnumerator Teleport(float nextTime)
    {
        yield return new WaitForSeconds(nextTime);
        gameObject.transform.position = new Vector3(148,1,13);
        yield return new WaitForSeconds(60.0f);
        page2SpawnPoint.SetActive(true);
        page2 = false;
    }
}
