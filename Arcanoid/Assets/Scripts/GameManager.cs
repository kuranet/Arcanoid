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
        foreach(var obstacle in LevelGenerator.obstacles)
        {
            float distance = Vector2.Distance(obstacle.position,ball.position);
            if(distance <= (obstacle.collisionRadius + ballRadius))
            {
                Debug.Log("Can be collision with " + obstacle.position.x + "   " + obstacle.position.y);
            }

        }
        float distanceE = Vector2.Distance(player.position, ball.position);
        float collisionRadius = Mathf.Sqrt( Mathf.Pow( player.localScale.x /2,2) + Mathf.Pow((player.localScale.y / 2) , 2));
        if (distanceE <= (collisionRadius + ballRadius))
        {
            Debug.Log("Can be collision with " + player.name);
        }
    }


}
