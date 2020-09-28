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

        Vector2 normal = (end - start).normalized;
        Debug.Log("NORMAL' " + normal.x + "   " + normal.y);

        //NEED TO FIX
        ///depends on end and start position of the boundary
        obj.transform.rotation = Quaternion.Euler(0, 0, 90 -Mathf.Acos(normal.x)*Mathf.Rad2Deg + 90);
    }
}
