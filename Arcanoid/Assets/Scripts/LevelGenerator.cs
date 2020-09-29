using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    static List<Obstacle> obstacles;

    void Awake()
    {
        obstacles = FileReader.ReadFromFile("Assets/Resources/level.txt");         
    }

    public static void CreateLevel()
    {
        foreach(Obstacle obstacle in obstacles)
        {
            obstacle.Create();
        }
    }
}
