using UnityEngine;

public class Block : Obstacle
{
    Vector2 position;

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
    }
}
