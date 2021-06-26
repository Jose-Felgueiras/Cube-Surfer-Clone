using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;


public class Surfer : MonoBehaviour
{
    int height = 0;
    private PathFollowerTest follower;
    private List<GameObject> cubesHeight = new List<GameObject>();


    private GameObject hitObj;
    public int Height
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
                height = value;
            }
            if (value < height)
            {
                if (height - value > 0)
                {
                    DecreaseHeight(value - height);
                    height = value;
                    Vector3 newCenter = Vector3.zero;
                    Vector3 newHeight = Vector3.one;
                    newCenter.x = ((height + 1) / 2) - .5f;
                    newHeight.x = height + 1;
                    GetComponent<BoxCollider>().center = newCenter;
                    GetComponent<BoxCollider>().size = newHeight;
                }
                
            }
            
        }
    }

    private void Start()
    {
        follower = GetComponentInParent<PathFollowerTest>();
    }

    void IncreaseHeight(int _value)
    {
        transform.localPosition = Vector3.left * (height + _value) + Vector3.left * .5f;
        for (int i = 0  ; i < _value; i++)
        {
            
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubesHeight.Add(cube);
            cube.layer = LayerMask.NameToLayer("HeightBlocks");
            Rigidbody rb = cube.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            rb.mass = 1000;
            rb.drag = 10;
            cube.transform.SetParent(transform);
            cube.transform.localPosition = Vector3.right * cubesHeight.Count;
            cube.transform.localRotation = Quaternion.Euler(0,0,0);
        }
        Vector3 newCenter = Vector3.zero;
        Vector3 newHeight = Vector3.one;
        newCenter.x = - ((transform.localPosition.x + .5f) / 2);
        newHeight.x = (height + _value) + 1;
        GetComponent<BoxCollider>().center = newCenter;
        GetComponent<BoxCollider>().size = newHeight;
    }

    void DecreaseHeight(int _value)
    {
        for (int i = 0; i > _value; i--)
        {
            cubesHeight[cubesHeight.Count - 1].transform.SetParent(null);
            cubesHeight.RemoveAt(cubesHeight.Count - 1);
        }
        for (int i = 0; i < cubesHeight.Count; i++)
        {
            cubesHeight[i].transform.localPosition = Vector3.right * (i + 1);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HeightBlock>())
        {
            if (hitObj != collision.gameObject)
            {
                hitObj = collision.gameObject;
                Height += collision.gameObject.GetComponent<HeightBlock>().height;
                Destroy(collision.gameObject.transform.parent.parent.gameObject);

            }
            
        }

        if (collision.gameObject.GetComponentInParent<WallObstacle>())
        {
            int[] offsetLimits = new int[2];
            offsetLimits[0] = Mathf.Clamp(Mathf.FloorToInt(follower.Offset + follower.offsetLimit - .5f), 0, 4);
            offsetLimits[1] = Mathf.Clamp(Mathf.CeilToInt(follower.Offset + follower.offsetLimit + .5f), 0, 4);

            int obstacleHeight = Mathf.Max(collision.gameObject.GetComponentInParent<WallObstacle>().offsetHeight[offsetLimits[0]], collision.gameObject.GetComponentInParent<WallObstacle>().offsetHeight[offsetLimits[1]]);
            if (height >= obstacleHeight)
            {
                if (hitObj != collision.gameObject)
                {
                    hitObj = collision.gameObject;
                    //PASS
                    Height -= obstacleHeight;
                }
            }
            else
            {
                follower.enabled = false;
                //GAME OVER
            }
        }
    }

    public void SetTowerCollision(bool _value)
    {
        foreach (GameObject segment in cubesHeight)
        {
            segment.GetComponent<BoxCollider>().enabled = _value;
        }
    }
}
