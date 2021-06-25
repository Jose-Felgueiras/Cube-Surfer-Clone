using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public enum EEntityType{
    OBSTACLE, CUBE, COIN
}
 [System.Serializable]
public class SpawnEntity
{
    public EEntityType type;
    public PathCreator path;
    public float offset;
    public Vector3 position;
    public GameObject prefab;
    public float dst;


    public SpawnEntity(EEntityType _type, Vector3 _position,int _id, float _dst)
    {
        type = _type;
        position = _position;
        dst = _dst;
        switch (_type)
        {
            case EEntityType.OBSTACLE:
                if (_id >= 0 && _id < PrefabsHolder.instance.obstaclePrefabs.Count)
                {
                    prefab = PrefabsHolder.instance.obstaclePrefabs[_id];
                }
                break;
            case EEntityType.CUBE:
                prefab = PrefabsHolder.instance.heightBlock;
                break;
            case EEntityType.COIN:
                break;
            default:
                break;
        }
    }

    public void Create()
    {

    }

}