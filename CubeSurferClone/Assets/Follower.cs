using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator creator;
    public float speed = 5f;
    float distanceTravelled;
    float offset;
    float offsetLimit = 2.5f;

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.rotation = creator.path.GetRotationAtDistance(distanceTravelled);
        transform.position = creator.path.GetPointAtDistance(distanceTravelled) + Vector3.up / 2 + offset * transform.up;
    }

    public void Move(float _value)
    {
        offset += _value;
        if (offset >= offsetLimit)
        {
            offset = offsetLimit;
        }
        if (offset <= -offsetLimit)
        {
            offset = -offsetLimit;
        }
    }


}
