using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Follower follower;
    Vector2 pressPosition;
    Vector2 offset;



    public void OnBeginDrag(PointerEventData _data)
    {
        pressPosition = _data.position;
        Debug.Log("BEGIN");
    }

    public void OnDrag(PointerEventData _data)
    {
        offset = _data.position - pressPosition;
        offset /= Screen.width;
        Debug.Log(offset);

        follower.Move(offset.x);
        Debug.Log("DRAAG");

    }

    public void OnEndDrag(PointerEventData _data)
    {
        Debug.Log("END");
                                     
    }
}
