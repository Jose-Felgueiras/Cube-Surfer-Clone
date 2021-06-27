using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using UnityEditor;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }

    [SerializeField, HideInInspector]
    PathCreator creator;
    [SerializeField, HideInInspector]
    RoadMeshCreator road;
    [SerializeField, HideInInspector]
    PathSpawnerTest spawner;
    [SerializeField]
    Level level;
    [SerializeField]
    PlayerController controller;

    [SerializeField]
    PathFollowerTest surfer;

    private void Start()
    {
        if (PlayerPrefs.GetInt("completedLevel", 0) >= PlayerPrefs.GetInt("currentLevel", 1))
        {
            PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel", 1) + 1);
            GenerateNewLevel(PlayerPrefs.GetInt("completedLevel", 1) + 1);
        }
        LoadLevel(level);
        UIManager.instance.UpdateUI();

        Camera.main.GetComponent<CameraFollow>().target = surfer;
    }

    public void LoadLevel(Level _level)
    {
        GameObject creatorPrefab = Instantiate(_level.creator.gameObject);
        GameObject spawnerPrefab = Instantiate(_level.spawner.gameObject);

        creator = creatorPrefab.GetComponent<PathCreator>();
        spawner = spawnerPrefab.GetComponent<PathSpawnerTest>();
        road = creatorPrefab.GetComponent<RoadMeshCreator>();
        creator.TriggerPathUpdate();
        road.TriggerUpdate();
        spawner.pathPrefab = creator;
        spawner.UpdateEntities();

        GameObject player = Instantiate(surfer.gameObject);
        surfer = player.GetComponent<PathFollowerTest>();
        surfer.pathCreator = creator;
        player.transform.position = level.creator.path.GetPointAtDistance(surfer.startDistanceTravelled, EndOfPathInstruction.Stop);
        player.transform.rotation = level.creator.path.GetRotationAtDistance(surfer.startDistanceTravelled, EndOfPathInstruction.Stop);
        controller.follower = surfer;
        surfer.enabled = false;
    }

    public void GenerateNewLevel(int _id)
    {

        level.creator.EditorData.ResetBezierPath(level.creator.transform.position, false);
        level.creator.bezierPath.Space = PathSpace.xz;
        level.creator.bezierPath.ControlPointMode = BezierPath.ControlMode.Automatic;
        int numSegments = 8;
        Vector3 prevPos = Vector3.zero;
        for (int i = 0; i < numSegments; i++)
        {
            Vector3 pos = new Vector3(Random.Range(10f, 100f), 0, Random.Range(-50f, 50f)) + prevPos;
            prevPos = pos;
            level.creator.bezierPath.AddSegmentToEnd(pos);
        }

        Vector3 lastPos =  Vector3.right * level.creator.path.length;
        level.creator.bezierPath.AddSegmentToEnd(lastPos);
        level.creator.bezierPath.AddSegmentToEnd(lastPos + Vector3.right * 50f);
        lastPos = Vector3.right * (level.creator.path.length + 100);
        level.creator.bezierPath.AddSegmentToEnd(lastPos);



        level.spawner.ClearData();
        float dstBetweenObstacles = 30f;
        float minDstBetweenCubes = 5f;
        int minBlocksToAdd = 0;


        int maxObstacles = (int)(level.creator.path.length / dstBetweenObstacles);
        for (int i = 1; i < maxObstacles; i++)
        {
            if (Random.value >= 0.5)
            {
                level.spawner.AddSpawnEntity(EEntityType.OBSTACLE, level.creator.path.GetPointAtDistance(dstBetweenObstacles * i), Random.Range(0, 5), dstBetweenObstacles * i);
                if (level.spawner.spawnEntities[level.spawner.spawnEntities.Count - 1].prefab.GetComponent<WallObstacle>())
                {
                    minBlocksToAdd += level.spawner.spawnEntities[level.spawner.spawnEntities.Count - 1].prefab.GetComponent<WallObstacle>().GetLowestPoint();
                }

                if (level.spawner.spawnEntities[level.spawner.spawnEntities.Count - 1].prefab.GetComponent<PitObstacle>())
                {
                    minBlocksToAdd += 5;
                }
               
            }

        }
        level.spawner.AddSpawnEntity(EEntityType.OBSTACLE, level.creator.path.GetPointAtDistance(level.creator.path.length - 40f, EndOfPathInstruction.Stop), 6, level.creator.path.length - 40f);


        minBlocksToAdd += Random.Range(15, 35);
        for (int i = 3; i < minBlocksToAdd + 3; i++)
        {
            int height = Random.Range(1, 4);
            float offset = Random.Range(-2f, 2f);
            float dst = Random.Range(minDstBetweenCubes, minDstBetweenCubes + 25);
            level.spawner.AddSpawnEntity(EEntityType.CUBE, level.creator.path.GetPointAtDistance(dst * i), 0, dst * i, height, offset);
            minBlocksToAdd -= height;
        }

    }



}
