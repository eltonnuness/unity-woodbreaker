using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
