﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void loadShopScene()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void exit()
    {
        Application.Quit();
    }
}
