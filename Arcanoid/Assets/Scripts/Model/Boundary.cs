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

        position = (start + end) / 2;

        boundaryPrefab = Resources.Load("Prefabs/Boundary");

        edgeCoords = new Vector2[4];
    }

    public override void Create()
    {
        GameObject obj = (GameObject)GameObject.Instantiate(boundaryPrefab);

        float scale = Vector2.Distance(start, end);
        obj.transform.localScale = new Vector3(scale, obj.transform.localScale.y);

        obj.transform.position = position;

        collisionRadius = Mathf.Sqrt(Mathf.Pow(obj.transform.localScale.x/2, 2) + Mathf.Pow(obj.transform.localScale.y/2, 2));

        Vector2 directionVector = (end - start).normalized;
        float angle = Vector2.SignedAngle(Vector2.right, directionVector);
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);
        angle *= Mathf.Deg2Rad;

        for(int i = 0; i< edgeCoords.Length;i++)
            edgeCoords[i] = position;

        edgeCoords[0] += obj.transform.localScale.x / 2 * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) + obj.transform.localScale.y / 2 * new Vector2(Mathf.Cos(Mathf.PI/2 + angle), Mathf.Sin(Mathf.PI / 2 + angle));
        edgeCoords[1] += obj.transform.localScale.x / 2 * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) - obj.transform.localScale.y / 2 * new Vector2(Mathf.Cos(Mathf.PI / 2 + angle), Mathf.Sin(Mathf.PI / 2 + angle));
        edgeCoords[2] += -obj.transform.localScale.x / 2 * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) - obj.transform.localScale.y / 2 * new Vector2(Mathf.Cos(Mathf.PI / 2 + angle), Mathf.Sin(Mathf.PI / 2 + angle));
        edgeCoords[3] += -obj.transform.localScale.x / 2 * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) + obj.transform.localScale.y / 2 * new Vector2(Mathf.Cos(Mathf.PI / 2 + angle), Mathf.Sin(Mathf.PI / 2 + angle));

    }
}
