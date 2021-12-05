using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;

    private static int currentScore = 0;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

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
        currentScore = 0;
    }

    public void PlayerScored()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();
    }
}
