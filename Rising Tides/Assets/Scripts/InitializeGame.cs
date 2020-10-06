using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitializeGame : MonoBehaviour
{

    public int gridSizeX = 5;
    public int gridSizeZ = 5;
    public int pillarInitHeight = -10;

    public float pillarNoiseOffset = 5;

    public GameObject pillar;
    public GameObject parent;

    GameObject[,] pillars;

    public int noiseStepSize = 3;

    private int pillarXSize;
    private int pillarZSize;
    private int pillarYSize;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InitializePillars();
    }

    void InitializePillars()
    {
        pillarXSize = (int)pillar.transform.localScale.x;
        pillarZSize = (int)pillar.transform.localScale.z;
        pillarYSize = (int)pillar.transform.localScale.y;

        pillars = new GameObject[gridSizeX * pillarXSize, gridSizeZ * pillarZSize];

        for (int x = 0; x < gridSizeX * pillarXSize; x += pillarXSize)
        {
            for (int z = 0; z < gridSizeZ * pillarZSize; z += pillarZSize)
            {
                Vector3 initPos = new Vector3(x - (gridSizeX * 2), pillarInitHeight - (pillarYSize / 2), z - (gridSizeZ * 2));

                GameObject newPillar = Instantiate(pillar, initPos, Quaternion.identity, parent.transform);
                pillars[x, z] = newPillar;

                pillarMovement pillarMovementScript = newPillar.GetComponent<pillarMovement>();

                float pillarOffset = Random.Range(0, pillarNoiseOffset);

                if (x == 0)
                {
                    pillarMovementScript.height = pillarInitHeight + z;
                }
                else
                {
                    pillarMovementScript.height = Random.Range(-noiseStepSize, noiseStepSize) + (pillarInitHeight + Mathf.Round(Mathf.PerlinNoise((this.transform.position.x + pillarOffset) / 10, (this.transform.position.y + pillarOffset) / 10) * 10) * noiseStepSize);
                }
            }
        }
    }

}