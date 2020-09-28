using UnityEngine;

public class Block : Obstacle
{
    Vector2 position;
    
    Object blockPrefab;

    public Block(Vector2 pos)
    {
        position = pos;
        blockPrefab = Resources.Load("Prefabs/Block");
    }

    public override void Create()
    {
        GameObject obj = (GameObject)GameObject.Instantiate(blockPrefab);
        obj.transform.position = position;
    }
}
