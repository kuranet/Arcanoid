using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    public static bool isGameStarted = false;
    [SerializeField] Transform ball;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            float playerLength = transform.localScale.x / 2;
            Vector2 target = new Vector2(touchPosition.x, transform.position.y);
            
            if (target.x + playerLength > ScreenUtils.ScreenRight)
                target = new Vector2(ScreenUtils.ScreenRight - playerLength, transform.position.y);
            else if(target.x - playerLength < ScreenUtils.ScreenLeft)
                target = new Vector2(ScreenUtils.ScreenLeft + playerLength, transform.position.y);
            
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (!isGameStarted)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    float heigh = 0.25f;
                    ball.position = transform.position + new Vector3(0, heigh, 0);
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    isGameStarted = true;
                    BallMovement.isMoving = true;
                    BallMovement.velocity = new Vector2(0, 1);
                }
            }
        }
    }

    private void OnEnable()
    {
        BallMovement.BallOutOfScreen += RespawnBall;
    }
    private void OnDisable()
    {
        BallMovement.BallOutOfScreen -= RespawnBall;
    }

    public void RespawnBall()
    {
        float heigh = 0.25f;
        ball.position = transform.position + new Vector3(0, heigh, 0);
        isGameStarted = false;
    }
}
