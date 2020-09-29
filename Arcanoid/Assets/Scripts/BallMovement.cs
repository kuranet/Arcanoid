﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public static Vector2 velocity;
    public float speed = 2f;

    public static bool isMoving = false; // to control if ball should move

    void FixedUpdate()
    {
        if (isMoving)
        {
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
    }
}
