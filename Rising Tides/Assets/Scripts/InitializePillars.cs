using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePillars : MonoBehaviour
{

    public float gridSizeX = 5f;
    public float gridSizeZ = 5f;
    public GameObject pillar;
    public GameObject parent;

    List<GameObject> pillars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
       for(int x = 0; x < gridSizeX; x++)
        {
            for(int z = 0; z < gridSizeZ; z++)
            {
                GameObject newPillar = Instantiate(pillar, new Vector3(x, 0, z), Quaternion.identity, parent.transform);
                pillars.Add(newPillar);
            }
        }            
    }

    private void Update()
    {
        
    }
}
