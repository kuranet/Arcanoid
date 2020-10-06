using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform ball;
    [SerializeField] Transform player;

    float ballRadius;


    public delegate void ActionHandler();
    public static event ActionHandler BreakBlock;

    void Start()
    {
        LevelGenerator.CreateLevel();
        ballRadius = ball.localScale.x / 2;
    }

    void FixedUpdate()
    {

        if (BallMovement.isMoving)
        {
            Vector2 position = ball.position;
            if ((position.x + ballRadius) > ScreenUtils.ScreenRight || (position.x - ballRadius) < ScreenUtils.ScreenLeft)
            {
                BallMovement.velocity.x *= -1;
                ChangeVelocity();
            }
            if ((position.y + ballRadius) > ScreenUtils.ScreenTop)
            {
                BallMovement.velocity.y *= -1;
                ChangeVelocity();
            }
            if (position.y < ScreenUtils.ScreenBottom)
            {
                if (BallOutOfScreen != null)
                    BallOutOfScreen();
                BallMovement.velocity = new Vector2(0, 0);
                ChangeVelocity();
            }
            Vector2 target = position + BallMovement.velocity;
            ball.position = Vector2.MoveTowards(ball.position, target, BallMovement.speed);
        }

        foreach (var obstacle in LevelGenerator.obstacles)
        {
            float distance = Vector2.Distance(obstacle.position,ball.position);
            if(distance <= (obstacle.collisionRadius + ballRadius))
            {
                
                if(!obstacle.isCheckedForCurrentVelocity || obstacle.curretnVelocity != BallMovement.velocity)
                {
                    Debug.Log("Get into area of collision : " + obstacle.position.x + "  " + obstacle.position.y);
                    object[,] list = new object[4, 3]; 
                    (list[0, 0], list[0, 1], list[0, 2]) = LineCross.IsCrossed(obstacle.edgeCoords[0], obstacle.edgeCoords[1], ball.position, BallMovement.velocity, ballRadius);
                    (list[1, 0], list[1, 1], list[1, 2]) = LineCross.IsCrossed(obstacle.edgeCoords[1], obstacle.edgeCoords[2], ball.position, BallMovement.velocity, ballRadius);
                    (list[2, 0], list[2, 1], list[2, 2]) = LineCross.IsCrossed(obstacle.edgeCoords[2], obstacle.edgeCoords[3], ball.position, BallMovement.velocity, ballRadius);
                    (list[3, 0], list[3, 1], list[3, 2]) = LineCross.IsCrossed(obstacle.edgeCoords[3], obstacle.edgeCoords[0], ball.position, BallMovement.velocity, ballRadius);

                    bool collides = false;
                    Vector2 point = new Vector2();
                    Vector2 normal = new Vector2();
                    float distanceToCollision = 0;

                    for(int i = 0; i < 4; i++)
                    {
                        if (!collides && (bool)list[i, 0])
                        {
                            collides = true;
                            point = (Vector2)list[i, 1];
                            normal = (Vector2)list[i, 2];
                            distanceToCollision = Vector2.Distance(ball.position, point);
                            obstacle.isFindPoint = true;
                        }
                        else if(collides && (bool)list[i, 0])
                        {

                            float tempDistane = Vector2.Distance(ball.position, (Vector2)list[i, 1]);
                            if (tempDistane < distanceToCollision)
                            {
                                point = (Vector2)list[i, 1];
                                normal = (Vector2)list[i, 2];
                                distanceToCollision = Vector2.Distance(ball.position, point);
                            }
                        }
                    }
                    if (collides)
                    {
                        obstacle.pointOfCollision = point;
                        obstacle.normalToCollision = normal;
                    }

                    Debug.Log("Can be collision with " + obstacle.pointOfCollision.x + "   " + obstacle.pointOfCollision.y);
                    obstacle.curretnVelocity = BallMovement.velocity;
                    obstacle.isCheckedForCurrentVelocity = true;
                }
                else if(obstacle.isCheckedForCurrentVelocity && obstacle.isFindPoint)
                {
                    float distanceToCollision = Vector2.Distance((Vector2)ball.position,obstacle.pointOfCollision);
                    //Debug.Log("Collision point " + obstacle.pointOfCollision.x + "  " + obstacle.pointOfCollision.y);
                    //Debug.Log(distanceToCollision);
                    if (distanceToCollision <= Vector2.Distance(Vector2.zero,BallMovement.velocity*BallMovement.speed))
                    {
                        Vector2 normal = obstacle.normalToCollision;
                        Debug.Log("NORMAL " + normal.x + "   "+normal.y);
                        BallMovement.velocity *= -1;
                        float angle = Vector2.SignedAngle(normal, BallMovement.velocity);
                        if (Mathf.Abs(angle) >= 90)
                        {
                            normal *= -1;
                            angle = Vector2.SignedAngle(normal, BallMovement.velocity);
                        }
                        BallMovement.velocity = Quaternion.Euler(0, 0, -2 * angle) * BallMovement.velocity;
                        ChangeVelocity();
                        Debug.Log("change velocity collides with " + obstacle.position.x + "  " + obstacle.position.y);
                    }
                }
            }
        }
        float distanceE = Vector2.Distance(player.position, ball.position);
        float collisionRadius = Mathf.Sqrt( Mathf.Pow( player.localScale.x /2,2) + Mathf.Pow((player.localScale.y / 2) , 2));
        if (distanceE <= (collisionRadius + ballRadius))
        {
            Debug.Log("Can be collision with " + player.name);
        }
    }

    public static event ActionHandler BallOutOfScreen;

    private void Awake()
    {
        ScreenUtils.Initialize();
        BallMovement.velocity = new Vector2(0, 1);
        BallMovement.isMoving = true;
        ballRadius = transform.localScale.x / 2;
    }
    void ChangeVelocity()
    {
        foreach(var obstacle in LevelGenerator.obstacles)
        {
            obstacle.isFindPoint = false;
            obstacle.isCheckedForCurrentVelocity = false;
        }
    }


}
