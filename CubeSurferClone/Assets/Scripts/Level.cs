using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class Level : ScriptableObject
{
    [SerializeField]
    int id;
    [SerializeField]
    public PathCreator creator;
    [SerializeField]
    public PathSpawnerTest spawner;
}
