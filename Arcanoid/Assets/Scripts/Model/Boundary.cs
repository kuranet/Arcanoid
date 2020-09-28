using UnityEngine;
public class Boundary : Obstacle
{
    Vector2 start;
    Vector2 end;

    Object boundaryPrefab;

    public Boundary(Vector2 st, Vector2 en)
    {
        start = st;
        end = en;

        boundaryPrefab = Resources.Load("Prefabs/Boundary");
    }

    public override void Create()
    {
        GameObject obj = (GameObject)GameObject.Instantiate(boundaryPrefab);

        float scale = Vector2.Distance(start, end);
        obj.transform.localScale = new Vector3(obj.transform.localScale.x, scale);

        Vector2 position = (end - start) / 2;
        obj.transform.position = position;
    }
}
