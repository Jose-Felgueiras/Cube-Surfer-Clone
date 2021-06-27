using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;


public class PathFollowerTest : PathFollower
{
    
    public float offsetLimit = 5.0f;
    float offset;

    public float Offset
    {
        get
        {
            return offset;
        }
    }
    

    protected override void Move()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction) + transform.up * offset;
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        UIManager.instance.UpdatePathProgress(distanceTravelled / pathCreator.path.length);
        if (distanceTravelled >= pathCreator.path.length)
        {
            Debug.Log("END");

            PlayerPrefs.SetInt("completedLevel", PlayerPrefs.GetInt("currentLevel", 1));
            UIManager.instance.NextLevel();
            this.enabled = false;
        }
    }

    public void PathSideOffset(float _value)
    {
        offset += _value;
        offset = Mathf.Clamp(offset, -offsetLimit, offsetLimit);
    }


}

