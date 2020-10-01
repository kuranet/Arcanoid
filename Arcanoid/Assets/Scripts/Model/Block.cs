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

        edgeCoords = new Vector2[4];
    }

    public override void Create()
    {
        GameObject obj = (GameObject)GameObject.Instantiate(blockPrefab);
        obj.transform.position = position;
        collisionRadius = Mathf.Sqrt(Mathf.Pow(obj.transform.localScale.x/2, 2) + Mathf.Pow(obj.transform.localScale.y/2, 2));

        float angle = Mathf.PI / 4;

        for (int i = 0; i < edgeCoords.Length; i++)
            edgeCoords[i] = position;

        edgeCoords[0] += collisionRadius * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) ;
        edgeCoords[1] += collisionRadius * new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
        edgeCoords[2] += collisionRadius * new Vector2(-Mathf.Cos(angle), -Mathf.Sin(angle));
        edgeCoords[3] += collisionRadius * new Vector2(-Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
