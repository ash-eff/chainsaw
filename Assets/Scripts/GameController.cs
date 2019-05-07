using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject gameOverScreen;

    private bool gameOver;

    public bool GameOver
    {
        get { return gameOver; }
        set
        {
            gameOver = value;
            gameOverScreen.SetActive(value);
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
