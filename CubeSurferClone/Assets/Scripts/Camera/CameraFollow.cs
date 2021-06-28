using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public PathFollowerTest target;

    public float smoothSpeed = 20f;

    Vector3 direction;
    float distance;
    float height;

    public float Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
            distance = Mathf.Clamp(distance, 16f, 30f);
        }
    
    }

    public float Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
            Vector3 newPos = new Vector3(-height, 0, 0);
        }
    }


    private void Start()
    {
        direction =   transform.localPosition;
        distance = direction.magnitude;
    }

    private void Update()
    {
           Vector3 desiredPosition = direction.normalized * distance + new Vector3(-height, 0, 0);
        Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.localPosition = smoothedPosition;
    }
}
