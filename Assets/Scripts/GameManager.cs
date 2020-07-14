﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); // Load Game Scene
        }

        if (Input.GetKeyDown(KeyCode.Q) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); // Load Main Menu Scene
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }


}
