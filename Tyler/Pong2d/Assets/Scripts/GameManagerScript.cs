using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    int playerOneScore, playerTwoScore;
    [SerializeField]
    int winningScore = 3;

    [SerializeField]
    BallScript gameBall;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    AudioClip goalScored;
    [SerializeField]
    AudioClip endGame;

    AudioSource audSource;

    [SerializeField]
    GameObject endGameScreen;

    // Use this for initialization
    void Start()
    {
        audSource = GetComponent<AudioSource>();
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoalScored(int playerNumber)
    {
        PlaySound(goalScored);

        if (playerNumber == 1)
        {
            playerOneScore++;
        }
        else
        {
            playerTwoScore++;
        }

        UpdateScoreText();


        if (playerOneScore == winningScore)
            GameOver(1);
        else if (playerTwoScore == winningScore)
            GameOver(2);
        else
            gameBall.Reset();
    }

    void GameOver(int winner)
    {
        PlaySound(endGame);
        endGameScreen.SetActive(true);
        gameBall.Stop();
    }

    public void StartNewGame()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
        UpdateScoreText();
        endGameScreen.SetActive(false);
        gameBall.Reset();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Player One " + playerOneScore.ToString() + " - " + playerTwoScore.ToString() + " Player Two";
    }

    void PlaySound(AudioClip soundClip)
    {
        audSource.clip = soundClip;
        audSource.Play();
    }
}