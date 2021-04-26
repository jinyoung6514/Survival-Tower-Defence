using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tower
{
    public string craftName;
    public GameObject tower_Prefab;  
    public GameObject tower_PreviewPrefab;
    
}
public class towerManager : MonoBehaviour
{
    bool isActivated =true;
    public bool isPreviewActivated = false;
    public GameObject towerManual;
    public Tower[] tower_asset;
    GameObject tower_Preview;
    GameObject Build_tower;
    Ray ray;
    Camera maincamera;
    Vector3 targetPos;
    RaycastHit hit;
    statusController _status;
    int _TowerNum;
    public GameObject _ShortageCoin;
    public GameObject _Disterror;
    Transform playerTr;
    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
        _status = GameObject.Find("UI").GetComponent<statusController>();
        playerTr = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = maincamera.ScreenPointToRay(Input.mousePosition);

        if(Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
        {
            Manual();
        }
        if(isPreviewActivated)
        {
            PreviewPositionUpdate();
        }
        if(Input.GetButtonDown("Fire1") && isPreviewActivated)
        {
            float dist =  Vector3.Distance(playerTr.position,hit.point);
            if(dist < 5.0f)
            {
                Build();
            }else 
            {
                Destroy(tower_Preview);
                isPreviewActivated = false;
                StartCoroutine(ShortDist(3.0f));
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
        
    }

    void Build()
    {
        if(isPreviewActivated)
        {
            if(_TowerNum == 0&& _status._coin >= 10)
            {
                Instantiate(Build_tower, hit.point, Quaternion.identity);
                Destroy(tower_Preview);
                isActivated = false;
                isPreviewActivated = false;
                Build_tower = null;
                tower_Preview = null;
                _status._coin -= 10;
            }
            else if(_TowerNum == 1 && _status._coin >=20)
            {
                Instantiate(Build_tower, hit.point, Quaternion.identity);
                Destroy(tower_Preview);
                isActivated = false;
                isPreviewActivated = false;
                Build_tower = null;
                tower_Preview = null;
                _status._coin -= 20;
            } 
            else if(_TowerNum == 2 && _status._coin >=1)
            {
                Instantiate(Build_tower, hit.point, Quaternion.identity);
                Destroy(tower_Preview);
                isActivated = false;
                isPreviewActivated = false;
                Build_tower = null;
                tower_Preview = null;
                _status._coin -= 1;
            }
            else{
                Destroy(tower_Preview);
                StartCoroutine(Shortcoin(3.0f));
                isPreviewActivated = false;
            }
            
        }
    }

    void PreviewPositionUpdate()
    {
        if(Physics.Raycast(ray, out hit, 10000f))
        {
            if(hit.transform != null && tower_Preview)
            {
                targetPos = hit.point;
                tower_Preview.transform.position = targetPos;
            }
        }
    }

    void Manual()
    {
        if(!isActivated)
        {
            OpenWindow();
        }
        else{
            CloseWindow();
        }
    }

    void OpenWindow()
    {
        isActivated = true;
        towerManual.SetActive(true);
    }

    void CloseWindow()
    {
        isActivated = false;
        towerManual.SetActive(false);
    }

    public void TowerClick(int _towerNum)
    {   
        _TowerNum = _towerNum;     
        tower_Preview = Instantiate(tower_asset[_towerNum].tower_PreviewPrefab,targetPos,Quaternion.identity);
        Build_tower = tower_asset[_towerNum].tower_Prefab;
        isPreviewActivated = true;
        towerManual.SetActive(false);
    }

    void Cancel()
    {
        if(isPreviewActivated)
        {
            Destroy(tower_Preview);
        }
        isActivated = false;
        isPreviewActivated = false;
        tower_Preview = null;
        Build_tower = null;

        towerManual.SetActive(false);
    }

    IEnumerator Shortcoin(float cointextTime)
    {
        _ShortageCoin.SetActive(true);
        yield return new WaitForSeconds(cointextTime);
        _ShortageCoin.SetActive(false);
    }
    IEnumerator ShortDist(float disttextTime)
    {
        _Disterror.SetActive(true);
        yield return new WaitForSeconds(disttextTime);
        _Disterror.SetActive(false);
    }

    
}
