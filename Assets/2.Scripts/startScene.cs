﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickStart()
    {
        Debug.Log("재시작!");
        SceneManager.LoadScene("Page1");
    }
    public void clickExit()
    {
        Debug.Log("종료");
        Application.Quit();
    }
}
