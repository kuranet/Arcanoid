﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    public static bool isGameStarted = false;
    [SerializeField] Transform ball;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Vector2 touchPosition = touch.position;
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            Vector2 target = new Vector2(touchPosition.x, transform.position.y);

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
}
