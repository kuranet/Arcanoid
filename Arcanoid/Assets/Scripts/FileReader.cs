using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileReader
{
    static public List<Obstacle> ReadFromFile(string path)
    {
        List<Obstacle> obstacles = new List<Obstacle>();

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
               string[] cordsStr = line.Split(';');

                float[] cordsFloat = new float[cordsStr.Length];

                for(int i = 0; i < cordsFloat.Length; i++)
                {
                    cordsFloat[i] = float.Parse(cordsStr[i]);
                }

                switch (cordsStr.Length)
                {
                    case 2: 
                        {
                            obstacles.Add(new Block(new Vector2(cordsFloat[0], cordsFloat[1])));
                            break; 
                        }
                    case 4: 
                        {
                            obstacles.Add(new Boundary(new Vector2(cordsFloat[0], cordsFloat[1]), new Vector2(cordsFloat[2], cordsFloat[3])));
                            break; 
                        }
                }
            }
        }       
        return obstacles;
    }
}
