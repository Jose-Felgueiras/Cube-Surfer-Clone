using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitObstacle : Obstacle
{
    PathFollowerTest follower;
    Surfer surfer;
    bool isSurferInside;
    BoxCollider collision;
    Vector3[] pointsToCheck = new Vector3[4];
    Vector3 newCenter = Vector3.zero;
    float newHeight = 0;
    public float sinkRate = 5.0f;
    PitObstacle()
    {
        type = EObstacleType.PIT;
    }

    private void Start()
    {
        collision = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Surfer>())
        {
            surfer = collision.gameObject.GetComponent<Surfer>();
            follower = collision.gameObject.GetComponentInParent<PathFollowerTest>();
        } 
    }
    private void FixedUpdate()
    {
        if (surfer)
        {
            pointsToCheck[0] = surfer.transform.position + surfer.transform.up * 0.5f + surfer.transform.forward * .5f;
            pointsToCheck[1] = surfer.transform.position - surfer.transform.up * 0.5f + surfer.transform.forward * .5f;
            pointsToCheck[2] = surfer.transform.position - surfer.transform.up * 0.5f - surfer.transform.forward * .5f;
            pointsToCheck[3] = surfer.transform.position + surfer.transform.up * 0.5f - surfer.transform.forward * .5f;

            pointsToCheck[0].y = 1;
            pointsToCheck[1].y = 1;
            pointsToCheck[2].y = 1;
            pointsToCheck[3].y = 1;


            if (!collision.bounds.Contains(pointsToCheck[0]) || !collision.bounds.Contains(pointsToCheck[1]) || !collision.bounds.Contains(pointsToCheck[2]) || !collision.bounds.Contains(pointsToCheck[3]))
            {
                if (isSurferInside)
                {
                    surfer.SetTowerCollision(true);
                    if (Mathf.Abs(Mathf.FloorToInt(newHeight)) >= surfer.Height)
                    {
                        //GAME OVER
                        follower.enabled = false;
                        return;
                    }
                    newHeight = Mathf.FloorToInt(newHeight);
                    surfer.transform.localPosition = Vector3.left * (surfer.Height + newHeight);

                    surfer.Height += Mathf.FloorToInt(newHeight);
                    isSurferInside = false;
                    return;
                }
            }
            else
            {
                isSurferInside = true;
            }
            if (isSurferInside)
            {
                surfer.SetTowerCollision(false);
                newHeight -= sinkRate * Time.fixedDeltaTime;
                surfer.transform.localPosition = Vector3.left * (surfer.Height + newHeight) + Vector3.left * .5f;

            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pointsToCheck[0], .1f);
        Gizmos.DrawSphere(pointsToCheck[1], .1f);
        Gizmos.DrawSphere(pointsToCheck[2], .1f);
        Gizmos.DrawSphere(pointsToCheck[3], .1f);
    }
}
