using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("I've created something");
        List<Obstacle> obstacles = new List<Obstacle>();
        obstacles.Add(new Boundary(new Vector2(0,0), new Vector2(0,1)));
        obstacles.Add(new Block(new Vector2(2, 3)));
        CreateLevel(obstacles);
    }

    void CreateLevel(List<Obstacle> obstacles)
    {
        foreach(Obstacle obstacle in obstacles)
        {
            obstacle.Create();
        }
    }
}
