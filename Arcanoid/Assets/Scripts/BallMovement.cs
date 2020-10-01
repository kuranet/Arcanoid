using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public static Vector2 velocity;
    public float speed = 2f;
    float radius;

    public static bool isMoving = false; // to control if ball should move

    public delegate void ActionHandler();
    public static event ActionHandler BallOutOfScreen;

    private void Awake()
    {
        ScreenUtils.Initialize();
        velocity = new Vector2(0, 0);
        radius = transform.localScale.x / 2;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 position = transform.position;
            if ((position.x + radius)> ScreenUtils.ScreenRight || (position.x - radius) < ScreenUtils.ScreenLeft)
                velocity.x *= -1;
            if ((position.y + radius) > ScreenUtils.ScreenTop)
                velocity.y *= -1;
            if (position.y < ScreenUtils.ScreenBottom)
            {
                if (BallOutOfScreen != null)
                    BallOutOfScreen();
                velocity = new Vector2(0, 0);
            }
            Vector2 target = (Vector2)transform.position + velocity;
            transform.position = Vector2.MoveTowards(transform.position, target, speed);
        }
    }
}
