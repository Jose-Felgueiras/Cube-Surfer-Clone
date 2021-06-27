using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public PathFollowerTest target;


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
            transform.position = target.transform.position + direction * distance;
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
            transform.localPosition += newPos;
        }
    }


    private void Start()
    {
        direction = target.transform.position - transform.position;
        distance = Vector3.Distance(target.transform.position, transform.position);
    }
}
