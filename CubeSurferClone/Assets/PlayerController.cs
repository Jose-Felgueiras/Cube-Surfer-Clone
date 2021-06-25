using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PathCreation;
using PathCreation.Examples;

public class PlayerController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PathFollowerTest follower;
    public float sensitivity = 2f;

    Vector2 pressPosition;
    float xOffset;


    public void OnBeginDrag(PointerEventData _data)
    {
        pressPosition = _data.position;
        Debug.Log("BEGIN");
        
    }

    public void OnDrag(PointerEventData _data)
    {
        if (_data.dragging)
        {
            xOffset = (_data.position - pressPosition).x / Screen.width;
            xOffset *= sensitivity;
            follower.PathSideOffset(xOffset);
            pressPosition = _data.position;

            Debug.Log("DRAAG");
        }
    }

    public void OnEndDrag(PointerEventData _data)
    {
        Debug.Log("END");
                                     
    }
}
