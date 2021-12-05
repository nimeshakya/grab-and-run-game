using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDiedSceneController : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
