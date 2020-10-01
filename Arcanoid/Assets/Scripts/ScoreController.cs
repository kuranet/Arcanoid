using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public delegate void ActionHandler();
    public static event ActionHandler LoadWin;
    public delegate void ActionRestartHandler(int n);
    public static event ActionRestartHandler LoadRestart;

    int lives = 3;
    int blockCount;

    [SerializeField] Text livesText;
    [SerializeField] Text blockCountText;

    private void OnEnable()
    {
        blockCountText.text = Block.count.ToString();
        livesText.text = lives.ToString();

        //BallMovement.BreakBlock += UpdateBlockScore;
        BallMovement.BallOutOfScreen += UpdateLiveScore;
    }
    private void OnDisable()
    {
        //BallMovement.BreakBlock -= UpdateBlockScore;
        BallMovement.BallOutOfScreen -= UpdateLiveScore;
        Block.count = 0;
        lives = 3;
    }

    private void UpdateBlockScore()
    {
        blockCount = --Block.count ;
        if (blockCount > 0)
        {
            blockCountText.text = blockCount.ToString();
        }        
        else
        {
            if (LoadWin != null)
                LoadWin();
        }
    }

    private void UpdateLiveScore() 
    {
        lives--;
        if (lives > 0)
        {
            livesText.text = lives.ToString();
        }
        else
        {
            if (LoadRestart != null)
                LoadRestart(blockCount);
        }
    }
}
