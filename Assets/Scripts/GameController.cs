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
    public GameObject pauseMenu;

    private bool gameOver;
    private bool paused;

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
        if (gameOver)
        {
            return;
        }

        GameAdjust();
        Pause();
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

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        pauseMenu.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
