using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class manager : MonoBehaviour
{

    public int gridSizeX = 5;
    public int gridSizeZ = 5;
    public int pillarInitHeight = -10;
    public int numOfObjectives = 5;
    public int noiseStepSize = 3;
    public int score = 0;

    public bool lockMouse = false;

    public float pillarNoiseOffset = 5;

    public GameObject pillar;
    public GameObject parent;
    public GameObject player;
    public GameObject objective;

    public Text scoreText; 

    GameObject[,] pillars;

    List<GameObject> objectives = new List<GameObject>();

    private int pillarXSize;
    private int pillarZSize;
    private int pillarYSize;

    // Start is called before the first frame update
    void Awake()
    {
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Initialize();
    }

    public void Initialize()
    {
        InitializePillars();
        InitializeObjectives();
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

                pillarMovementScript.player = player;

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

    void InitializeObjectives()
    {

        List<Vector2> availableIndices = new List<Vector2>();
        List<Vector2> chosenIndices = new List<Vector2>();

        bool validIndices = false;

        for(int i =0; i < pillars.Length; i++)
        {
            int X;
            int Z;
            while (validIndices == false)
            {
                X = Random.Range(0, gridSizeX * pillarXSize);
                Z = Random.Range(0, gridSizeZ * pillarZSize);
                if(pillars[X, Z] != null)
                {
                    availableIndices.Add(new Vector2(X, Z));
                    validIndices = true;
                }
            }
            validIndices = false;
        }

        for(int i = 0; i < numOfObjectives; i++)
        {
            int index = Random.Range(1, availableIndices.Count);
            chosenIndices.Add(availableIndices[index]);
            availableIndices.RemoveAt(index);
        }

        for(int i = 0; i < chosenIndices.Count; i++)
        {
            int objX = (int)chosenIndices[i].x;
            int objY = (int)chosenIndices[i].y;
            Vector3 pos = pillars[objX,objY].transform.position;

            pos.y += (pillarYSize / 2 + objective.transform.localScale.y / 2);
            GameObject newObj = Instantiate(objective, pos, Quaternion.identity);

            newObj.transform.SetParent(pillars[objX, objY].transform);
            newObj.GetComponentInChildren<objectiveInteraction>().gameManager = this.gameObject.GetComponent<manager>();
            newObj.GetComponentInChildren<objectiveInteraction>().sceneManager = this.gameObject.GetComponent<SceneManager>();

            objectives.Add(newObj);
        }
    }

    public void updateScore(int currentScore, float possibleScore)
    {
        score += currentScore;
        scoreText.text = "Score: " + score.ToString() + " / " + possibleScore.ToString();
    }

}