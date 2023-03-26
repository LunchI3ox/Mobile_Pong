using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject menuObject;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI volumeValueText;
    public TextMeshProUGUI playModeButtonText;

    public System.Action onStartGame;



    private void Start()
    {
        AdjustPlayModeButtonText();
    }
    public void HighlightScore(int player)
    {
        if (player == 1)
        {
            scoreTextPlayer1.Highlight();
        }
        else
        {
            scoreTextPlayer2.Highlight();
        }
    }



    public void UpdateScores(int scorePlayer1, int scorePlayer2)
    {
        scoreTextPlayer1.SetScore(scorePlayer1);
        scoreTextPlayer2.SetScore(scorePlayer2);
    }

    public void OnStartButtonClick()
    {
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winnerID)
    { 
        menuObject.SetActive(true);
        winText.text = $"Player {winnerID} wins!";
    }
    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        volumeValueText.text = $"{Mathf.RoundToInt(value * 100)}%";
    }

    public void OnSwitchPlayModeButtonClicked()
    {
        GameManager.instance.SwitchPlayMode();
        AdjustPlayModeButtonText();
    }


    private void AdjustPlayModeButtonText()
    {

        switch (GameManager.instance.playMode)
        {
            case GameManager.PlayMode.PlayerVsPlayer:
                playModeButtonText.text = "2 Player";
                break;

            case GameManager.PlayMode.PlayerVsAi:
                playModeButtonText.text = "Player vs AI";
                break;
            case GameManager.PlayMode.AiVsAi:
                playModeButtonText.text = "Ai vs AI";
                break;
        }
    }
}
