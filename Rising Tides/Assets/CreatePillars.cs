using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePillars : MonoBehaviour
{
    public GameObject pillar;
    public float gridSize = 5;

    List<GameObject> pillarList = new List<GameObject>();
    private void Start()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                GameObject currentPillar = Instantiate(pillar, new Vector3(x, 0, z), Quaternion.identity);
                pillarList.Add(currentPillar);
            }
        }
    }
    
}
