using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePillars : MonoBehaviour
{

    public float gridSize = 5f;
    public GameObject pillar;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
       for(int x = 0; x < gridSize; x++)
        {
            for(int z = 0; z < gridSize; z++)
            {
                Instantiate(pillar, new Vector3(x, 0, z), Quaternion.identity, parent.transform);
            }
        }            
    }
}
