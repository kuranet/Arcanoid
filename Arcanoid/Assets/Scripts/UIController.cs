using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] Text loseText;

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
                        Touch touch = Input.touches[0];
                        if (touch.phase == TouchPhase.Moved)
                        {
                            gameState = GameState.PLAYING;
                            StartPanel.SetActive(false);
                            ScorePanel.SetActive(true);
                            LevelGenerator.CreateLevel();

                            PlayerMovement.isGameStarted = false;
                        }
                    }
                    break;
                }
            case GameState.RESRTART:
                {
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.touches[0];
                        if (touch.phase == TouchPhase.Ended)
                            SceneManager.LoadScene("SampleScene");
                    }
                    break;
                }
        }
    }
    private void OnEnable()
    {
        ScoreController.LoadRestart += LoadResrartPanel;
        ScoreController.LoadWin += LoadWinPanel;

    }
    private void OnDisable()
    {
        ScoreController.LoadRestart -= LoadResrartPanel;
        ScoreController.LoadWin -= LoadWinPanel;
    }

    void LoadResrartPanel(int n) 
    {
        ScorePanel.SetActive(false);
        RestartPanel.SetActive(true);
        loseText.text = n.ToString();
        gameState = GameState.RESRTART;
    }
    void LoadWinPanel()
    {
        ScorePanel.SetActive(false);
        WinPanel.SetActive(true);
        gameState = GameState.RESRTART; 
    }
}
