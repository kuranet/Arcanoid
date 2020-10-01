using UnityEngine;

public class Block : Obstacle
{
    Object blockPrefab;

    public static int count = 0;

    public Block(Vector2 pos)
    {
        count++;
        position = pos;
        blockPrefab = Resources.Load("Prefabs/Block");
    }

    public override void Create()
    {
        GameObject obj = (GameObject)GameObject.Instantiate(blockPrefab);
        obj.transform.position = position;
        collisionRadius = Mathf.Sqrt(Mathf.Pow(obj.transform.localScale.x/2, 2) + Mathf.Pow(obj.transform.localScale.y/2, 2));
    }
}
