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

        float localScale = obj.transform.localScale.x / 2;

        for (int i = 0; i < edgeCoords.Length; i++)
            edgeCoords[i] = position;

        edgeCoords[0] += new Vector2(localScale,localScale);
        edgeCoords[1] += new Vector2(localScale, -localScale);
        edgeCoords[2] += new Vector2(-localScale, -localScale);
        edgeCoords[3] += new Vector2(-localScale, localScale);
    }
}
