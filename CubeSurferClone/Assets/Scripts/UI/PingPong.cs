using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingPong : MonoBehaviour
{

    Vector3 startPos;
    public float distance = 100f;
    public float speed = 5f;

    void Start()
    {
        startPos = GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().position = new Vector3(startPos.x + Mathf.PingPong(Time.time * speed, distance), startPos.y, startPos.z);
    }
}
