using UnityEngine;
public abstract class Obstacle 
{
    public Vector2 position { get; protected set; }

    public float collisionRadius { get; protected set; }

    public Vector2[] edgeCoords { get; protected set; }

    public bool isCheckedForCurrentVelocity = false;
    public Vector2 curretnVelocity;

    public Vector2 pointOfCollision;

    public virtual void Create()
    {

    }
}
