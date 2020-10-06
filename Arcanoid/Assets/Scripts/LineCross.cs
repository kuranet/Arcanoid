using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCross : MonoBehaviour
{
    public static (bool,Vector2,Vector2) IsCrossed(Vector2 start, Vector2 end, Vector2 centralPosition, Vector2 velocity, float ballRadius)
    {
        /// (Ye - Ys) * X - (Xe - Xs) * Y - (Ye - Ys) * Xe  + (Xe - Xs) * Ye = 0
        /// (Yn - Yc) * X - (Xn - Xc) * Y - (Yn - Yc) * Xn  + (Xn - Xc) * Yn = 0
        
        Vector2 positionNext = (Vector2) centralPosition + velocity;

        float denominator = (end.y - start.y) * (positionNext.x - centralPosition.x) - (positionNext.y - centralPosition.y) * (end.x - start.x);

        if (denominator != 0)
        {
            Vector2 delta = Quaternion.Euler(0, 0, 90) * velocity * ballRadius;
            Vector2 rightPoint = centralPosition + delta;
            Vector2 rightNextPoint = rightPoint + velocity;

            delta = Quaternion.Euler(0, 0, -90) * velocity * ballRadius;
            Vector2 leftPoint = centralPosition + delta;
            Vector2 leftNextPoint = leftPoint + velocity;

            float denominatorRight = (end.y - start.y) * (rightNextPoint.x - rightPoint.x) - (rightNextPoint.y - rightPoint.y) * (end.x - start.x);
            float denominatorLeft = (end.y - start.y) * (leftNextPoint.x - leftPoint.x) - (leftNextPoint.y - leftPoint.y) * (end.x - start.x);

            // float X = -((-(end.y - start.y) * end.x + (end.x - start.x) * end.y) * (positionNext.x - positionCurrent.x) - (-(positionNext.y - positionCurrent.y) * positionNext.x + (positionNext.x - positionCurrent.x) * positionNext.y) * (end.x - start.x)) / denominator;
            // float Y = ((-(positionNext.y - positionCurrent.y) * positionNext.x + (positionNext.x - positionCurrent.x) * positionNext.y) * (end.y - start.y) - (positionNext.y - positionCurrent.y) * (-(end.y - start.y) * end.x + (end.x - start.x) * end.y)) / denominator;

            float rightX = -((-(end.y - start.y) * end.x + (end.x - start.x) * end.y) * (rightNextPoint.x - rightPoint.x) - (-(rightNextPoint.y - rightPoint.y) * rightNextPoint.x + (rightNextPoint.x - rightPoint.x) * rightNextPoint.y) * (end.x - start.x)) / denominatorRight;
            float rightY = ((-(rightNextPoint.y - rightPoint.y) * rightNextPoint.x + (rightNextPoint.x - rightPoint.x) * rightNextPoint.y) * (end.y - start.y) - (rightNextPoint.y - rightPoint.y) * (-(end.y - start.y) * end.x + (end.x - start.x) * end.y)) / denominatorRight;

            float leftX = -((-(end.y - start.y) * end.x + (end.x - start.x) * end.y) * (leftNextPoint.x - leftPoint.x) - (-(leftNextPoint.y - leftPoint.y) * leftNextPoint.x + (leftNextPoint.x - leftPoint.x) * leftNextPoint.y) * (end.x - start.x)) / denominatorLeft;
            float leftY = ((-(leftNextPoint.y - leftPoint.y) * leftNextPoint.x + (leftNextPoint.x - leftPoint.x) * leftNextPoint.y) * (end.y - start.y) - (leftNextPoint.y - leftPoint.y) * (-(end.y - start.y) * end.x + (end.x - start.x) * end.y)) / denominatorLeft;

            bool consist = false;
            Vector2 collisionPoint = new Vector2();
            Vector2 normal = Quaternion.Euler(0,0,90)*(end-start).normalized;

            Vector2 vec = new Vector2(rightX, rightY) - rightPoint;
            float angle = Vector2.Angle(velocity, vec);

            if (angle == 0)
                if (((rightX <= end.x && rightX >= start.x) || (rightX >= end.x && rightX <= start.x))|| (leftX <= end.x && leftX >= start.x) || (leftX >= end.x && leftX <= start.x))
                {
                    if ((rightY <= end.y && rightY >= start.y) || (rightY >= end.y && rightY <= start.y)||( (leftY <= end.y && leftY >= start.y) || (leftY >= end.y && leftY <= start.y)))
                    {
                        //Debug.Log("IT COLLIDES");
                        consist = true;

                        float X = -((-(end.y - start.y) * end.x + (end.x - start.x) * end.y) * (positionNext.x - centralPosition.x) - (-(positionNext.y - centralPosition.y) * positionNext.x + (positionNext.x - centralPosition.x) * positionNext.y) * (end.x - start.x)) / denominator;
                        float Y = ((-(positionNext.y - centralPosition.y) * positionNext.x + (positionNext.x - centralPosition.x) * positionNext.y) * (end.y - start.y) - (positionNext.y - centralPosition.y) * (-(end.y - start.y) * end.x + (end.x - start.x) * end.y)) / denominator;

                        angle = Vector2.Angle(end - start, new Vector2(X, Y) - centralPosition);
                        //Debug.Log("ANGLE " + angle);
                        if (angle != 90)
                        {
                            float hipotenuza = Mathf.Abs( ballRadius / Mathf.Sin(angle * Mathf.Deg2Rad));
                            //Debug.Log("HIP " + hipotenuza);
                            //Debug.Log("POINT " + X + "  " + Y);
                            collisionPoint = new Vector2(X, Y) - hipotenuza *(velocity);
                        }
                        else
                        {
                            float hipotenuza = ballRadius;
                            collisionPoint = new Vector2(X, Y) - hipotenuza *(velocity);
                        }
                    }
                }

            return (consist, collisionPoint, normal);
        }
        else return (false, new Vector2(), new Vector2()); 
    }
}
