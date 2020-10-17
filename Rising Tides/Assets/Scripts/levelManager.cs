using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public playerMovement playerScript;
    public manager managerScript;
    public pillarMovement pillarScript;
    public loadingScenes sceneLoader;

    public float destroyChanceChange = 10;
    public float rumbleLengthChange = 0.5f;
    public float objectiveCountChange = 2f;

    public int level = 0;
    public int initialObjectives = 1;
    public int finalLevel = 4;

    private void Start()
    {
        initialObjectives = managerScript.numOfObjectives;
        managerScript.updateScore(0);
    }

    private void Update()
    {
        if(level == finalLevel + 1)
        {
            sceneLoader.win();
        }
    }

    public void progressLevels()
    {
        if (managerScript.score == managerScript.numOfObjectives)
        {
            playerScript.reset = true;
            playerScript.movementEnabled = false;
            playerScript.animator.SetBool("running", false);
            playerScript.animator.SetBool("jumping", false);

            level++;

            managerScript.score = 0;
            increaseVariables();


            destroyPrefabs();
            managerScript.Initialize();
            playerScript.movementEnabled = true;
        }
    }

    void increaseVariables()
    {
        pillarScript.destroyChance += destroyChanceChange;
        pillarScript.rumbleLength -= rumbleLengthChange;

        managerScript.numOfObjectives += (int)objectiveCountChange;
        managerScript.updateScore(0);
    }

    void destroyPrefabs()
    {

        foreach(GameObject pillar in managerScript.pillars) { 
                GameObject.Destroy(pillar);
        }

        foreach (GameObject objective in managerScript.objectives)
        {
            GameObject.Destroy(objective);
        }
    }
}
