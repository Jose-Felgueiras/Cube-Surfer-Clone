using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacle : Obstacle
{
    public int[] offsetHeight = new int[5];

    public bool isFinish;

    WallObstacle()
    {
        type = EObstacleType.WALL;
    }

    public int GetLowestPoint()
    {
        int lowest = 10;
        for (int i = 0; i < offsetHeight.Length; i++)
        {
            if (offsetHeight[i] < lowest)
            {
                lowest = offsetHeight[i];
            }
        }

        return lowest;
    }
}
