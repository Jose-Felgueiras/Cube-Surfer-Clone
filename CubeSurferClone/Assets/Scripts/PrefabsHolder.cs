using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsHolder : MonoBehaviour
{

    public static PrefabsHolder instance;
    [SerializeField]
    public List<GameObject> obstaclePrefabs = new List<GameObject>();
    [SerializeField]
    public GameObject coinPrefabs;
    [SerializeField]
    public GameObject heightBlock;


    public PrefabsHolder  ()
    {
        if (!instance)
        {
            instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
