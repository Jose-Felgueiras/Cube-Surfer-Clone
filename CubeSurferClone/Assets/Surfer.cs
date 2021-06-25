using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfer : MonoBehaviour
{
    int height = 1;
    private List<GameObject> cubesHeight = new List<GameObject>();

    int Height
    {
        get
        {
            return height;
        }
        set
        {
            if (value > height)
            {
                IncreaseHeight(value - height);
            }
            if (value < height)
            {
                DecreaseHeight(value - height);
            }
            height = value;
            
        }
    }

    void IncreaseHeight(int _value)
    {
        
        for (int i = 0; i < _value; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubesHeight.Add(cube);
            cube.transform.SetParent(transform);
            cube.transform.localPosition = Vector3.left * cubesHeight.Count;
            cube.transform.localRotation = Quaternion.Euler(0,0,0);
        }
    }

    void DecreaseHeight(int _value)
    {
        cubesHeight.RemoveRange(cubesHeight.Count + _value - 1, _value);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<HeightBlock>())
        {
            IncreaseHeight(1);
            Destroy(other.gameObject);
        }
    }
}
