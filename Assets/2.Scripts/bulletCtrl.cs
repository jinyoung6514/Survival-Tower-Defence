﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCtrl : MonoBehaviour
{
    //총알의 파괴력
    public float damage = 20.0f;
    //총알의 발사속도
    public float speed = 1000.0f;
    [SerializeField]
    float DestroyTime = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward*speed);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, DestroyTime);
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider)
        {
            Destroy(gameObject);
            Debug.Log("총알!");
        }
    }
}
