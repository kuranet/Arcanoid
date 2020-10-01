using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCross : MonoBehaviour
{
    public static (bool,Vector2,Vector2) IsCrossed(Vector2 start, Vector2 end, Vector2 positionCurrent, Vector2 positionNext)
    {
        /// (Ye - Ys) * X - (Xe - Xs) * Y - (Ye - Ys) * Xe  + (Xe - Xs) * Ye = 0
        /// (Yn - Yc) * X - (Xn - Xc) * Y - (Yn - Yc) * Xn  + (Xn - Xc) * Yn = 0

        ///  

        float denominator = (end.y - start.y) * (positionNext.x - positionCurrent.x) - (positionNext.y - positionCurrent.y) * (end.x - start.x);

        if (denominator != 0)
        {
            float X = ((-(end.y - start.y) * end.x + (end.x - start.x) * end.y) * (positionNext.x - positionCurrent.x) - (-(positionNext.y - positionCurrent.y) * positionNext.x + (positionNext.x - positionCurrent.x) * positionNext.y) * (end.x - start.x)) / denominator;
            float Y = ((-(positionNext.y - positionCurrent.y) * positionNext.x + (positionNext.x - positionCurrent.x) * positionNext.y) * (end.y - start.y) - (positionNext.y - positionCurrent.y) * (-(end.y - start.y) * end.x + (end.x - start.x) * end.y)) / denominator;


            bool consist = false;
            Vector2 collisionPoint = new Vector2();
            Vector2 normal = new Vector2();

            if ((X <= end.x && X >= start.x) || (X >= end.x && X <= start.x))
            {
                if ((Y <= end.y && Y >= start.y) || (Y >= end.y && Y <= start.y))
                {
                    consist = true;
                    collisionPoint = new Vector2(X, Y);
                    normal = Quaternion.Euler(0, 0, 90) * (end - start);
                }
            }

            return (consist, collisionPoint,normal);
        }
        else return (false, new Vector2(), new Vector2()); 
    }
}
