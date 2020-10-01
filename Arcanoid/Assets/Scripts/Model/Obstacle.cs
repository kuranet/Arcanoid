using UnityEngine;
public abstract class Obstacle 
{
    public Vector2 position { get; protected set; }

    public float collisionRadius { get; protected set; }

    public Vector2[] edgeCoords { get; protected set; }

    public virtual void Create()
    {

    }
}
