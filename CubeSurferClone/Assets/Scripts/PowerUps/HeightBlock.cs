using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightBlock : MonoBehaviour
{
    SpawnEntity entity;

    public int height;

    SpawnEntity Entity
    {
        get
        {
            return entity;
        }
        set
        {
            entity = value;
        }
    }
}
