using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PathCreation;
using PathCreation.Utility;
using PathCreationEditor;

[CustomEditor(typeof(PathSpawnerTest))]
public class PathSpawnerEditor : Editor
{
    const float screenPolylineMaxAngleError = .3f;
    const float screenPolylineMinVertexDst = .01f;

    ScreenSpacePolyLine.MouseInfo pathMouseInfo;
    ScreenSpacePolyLine screenSpaceLine;
    bool hasUpdatedScreenSpaceLine;

    PathSpawnerTest spawner;
    VertexPath Path
    {
        get
        {
            return spawner.pathPrefab.path;
        }
    }
    private void OnSceneGUI()
    {
        if (spawner.pathPrefab)
        {
            Input();
        }
    }

    void Input()
    {
        Event guiEvent = Event.current;
        hasUpdatedScreenSpaceLine = false;
        UpdatePathMouseInfo();
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(spawner, "Added Entity");
            Vector3 newPathPoint = pathMouseInfo.closestWorldPointToMouse;
            spawner.AddSpawnEntity(spawner.selectedType, newPathPoint, 0, pathMouseInfo.distanceAlongPathWorld);
            spawner.UpdateEntities();
        }
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.control)
        {
            Undo.RecordObject(spawner, "Removed Entity");
            Vector3 newPathPoint = pathMouseInfo.closestWorldPointToMouse;
            spawner.RemoveEntity(spawner.GetClosestEntityToPoint(newPathPoint));
            spawner.UpdateEntities();
        }
        HandleUtility.AddDefaultControl(0);

    }

    private void OnEnable()
    {
        spawner = (PathSpawnerTest)target;
    }

    void UpdatePathMouseInfo()
    {
        if (!hasUpdatedScreenSpaceLine || (screenSpaceLine != null && screenSpaceLine.TransformIsOutOfDate()))
        {
            screenSpaceLine = new ScreenSpacePolyLine(bezierPath, spawner.pathPrefab.transform, screenPolylineMaxAngleError, screenPolylineMinVertexDst);
            hasUpdatedScreenSpaceLine = true;
        }
        pathMouseInfo = screenSpaceLine.CalculateMouseInfo();
    }

    BezierPath bezierPath
    {
        get
        {
            return data.bezierPath;
        }
    }

    PathCreatorData data
    {
        get
        {
            return spawner.pathPrefab.EditorData;
        }
    }
}
