using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    enum GameState
    {
        START,
        PLAYING,
        RESRTART
    }


    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject ScorePanel;
    [SerializeField] GameObject RestartPanel;
    [SerializeField] GameObject WinPanel;

    GameState gameState = GameState.START;

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.START:
                {
                    if (Input.touchCount > 0)
                    {
                        gameState = GameState.PLAYING;
                        StartPanel.SetActive(false);
                        ScorePanel.SetActive(true);
                        LevelGenerator.CreateLevel();
                    }
                    break;
                }
            case GameState.RESRTART:
                {
                    if (Input.touchCount > 0)
                    {
                        SceneManager.LoadScene("SampleScene"); 
                    }
                    break;
                }
        }
    }
    void UpdateLives()
    {

    }
}
