using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int changeTimer;
    public int adjustAmount;
    public float startTimer;
    public  float spawn;

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

    public float SpawnTime
    {
        get { return spawn; }
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

        spawn = startTimer;
    }

    private void Update()
    {
        GameAdjust();
    }

    void GameAdjust()
    {
        if (Time.timeSinceLevelLoad > changeTimer)
        {
            changeTimer += adjustAmount;
            float spawnAdj = spawn - .25f;

            if (spawn < .5f)
            {
                spawn = .5f;
            }
            else
            {
                spawn = spawnAdj;
            }
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
