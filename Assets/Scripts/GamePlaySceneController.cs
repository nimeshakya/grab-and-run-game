using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySceneController : MonoBehaviour
{
    private void OnEnable()
    {
        Player.playerDiedInfo += PlayerDiedListener;
    }

    private void OnDisable()
    {
        Player.playerDiedInfo -= PlayerDiedListener;
    }

    void PlayerDiedListener()
    {
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("PlayerDiedScene");
    }
}
