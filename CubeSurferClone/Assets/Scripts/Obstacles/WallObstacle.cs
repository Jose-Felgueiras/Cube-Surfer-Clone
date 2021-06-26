using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacle : Obstacle
{
    public int[] offsetHeight = new int[5];

    WallObstacle()
    {
        type = EObstacleType.WALL;
    }
}
