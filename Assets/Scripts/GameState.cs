using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private bool _shouldReload;

    private void Update()
    {
        GetInput();
        HandleInput();
    }

    private void HandleInput()
    {
        if (_shouldReload)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void GetInput()
    {
        if (Input.GetButton("Fire2") && !_shouldReload)
        {
            _shouldReload = true;
        }
    }
}
