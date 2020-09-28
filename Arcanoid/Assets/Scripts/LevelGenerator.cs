using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    void Awake()
    {
        List<Obstacle> obstacles = FileReader.ReadFromFile("Assets/Resources/level.txt");         
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
