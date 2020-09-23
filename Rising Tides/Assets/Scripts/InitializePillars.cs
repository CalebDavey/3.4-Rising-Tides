using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePillars : MonoBehaviour
{

    public int gridSizeX = 5;
    public int gridSizeZ = 5;

    public float maxHeight = 5;
    public float pillarNoiseOffset = 5;

    public GameObject pillar;
    public GameObject parent;
    public shake virtualCam;

    GameObject[,] pillars;

    // Start is called before the first frame update
    void Awake()
    {
        pillars = new GameObject[gridSizeX, gridSizeZ];

        for (int x = 0; x < gridSizeX; x++)
        {
            for(int z = 0; z < gridSizeZ; z++)
            {
                GameObject newPillar = Instantiate(pillar, new Vector3(x, 0, z), Quaternion.identity, parent.transform);
                pillars[x,z] = newPillar;

                pillarMovement pillarMovementScript = newPillar.GetComponent<pillarMovement>();

                pillarMovementScript.camShake = virtualCam;

                float pillarOffset = Random.Range(0, pillarNoiseOffset);
                
                pillarMovementScript.height = Mathf.Round(Mathf.PerlinNoise((this.transform.position.x + pillarOffset) / 10, (this.transform.position.y + pillarOffset) / 10) * 10);
            }
        }            
    }

}
