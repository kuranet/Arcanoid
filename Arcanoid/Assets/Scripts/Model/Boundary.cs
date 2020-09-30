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
        obj.transform.localScale = new Vector3(scale, obj.transform.localScale.y);

        Vector2 position = (start + end) / 2;
        obj.transform.position = position;

        Vector2 directionVector = (end - start).normalized;
        obj.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, directionVector));
    }
}
