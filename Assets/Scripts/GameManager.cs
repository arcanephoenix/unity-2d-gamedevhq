using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
    
}
