using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static int totalBlocks;
    public static int totalDestroyedBlocks;
    public Image stars;
    public GameObject canvas;
    public static GameController instance;
    public GameObject ball;
    public Platform platform;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (Application.loadedLevel == 1)
        {
            canvas.SetActive(false);
            totalDestroyedBlocks = 0;
        }
    }

    public void GameOver()
    {
        canvas.SetActive(true);
        stars.fillAmount = (float)totalDestroyedBlocks / (float)totalBlocks;
        platform.enabled = false;
        Destroy(ball);
    }

    public void changeScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void closeApp()
    {
        Application.Quit();
    }

}
