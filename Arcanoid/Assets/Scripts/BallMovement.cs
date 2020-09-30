using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public static Vector2 velocity;
    public float speed = 2f;
    float radius;

    public static bool isMoving = false; // to control if ball should move

    public delegate void ActionHandler();
    public static event ActionHandler BallOutOfScreen;
    public static event ActionHandler BreakBlock;

    private void Awake()
    {
        ScreenUtils.Initialize();
        velocity = new Vector2(0, 0);
        radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];

        velocity *= -1;
        float angle = Vector2.SignedAngle(contact.normal,velocity);

        velocity = Quaternion.Euler(0, 0, -2*angle) * velocity;
        
        angle = Vector2.SignedAngle(contact.normal, velocity);

        if(collision.collider.tag == "Block")
        {
            if (BreakBlock != null)
                BreakBlock();
            Destroy(collision.gameObject);
        }
    }
}
