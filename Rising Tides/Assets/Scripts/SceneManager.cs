using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public playerMovement playerScript;
    public manager managerScript;
    public pillarMovement pillarScript;

    public float destroyChanceChange = 10;
    public float rumbleLengthChange = 0.5f;
    public float objectiveCountChange = 2f;

    public int level = 0;
    public int initialObjectives = 0;

    private void Start()
    {
        initialObjectives = managerScript.numOfObjectives;
    }
    public void progressLevels()
    {
        if (managerScript.score == initialObjectives + (level * objectiveCountChange))
        {
            playerScript.movementEnabled = false;
            playerScript.animator.SetBool("running", false);
            playerScript.animator.SetBool("jumping", false);

            destroyPrefabs();
            managerScript.Initialize();
            playerScript.resetPlayer();
            level++;
            playerScript.movementEnabled = true;
        }
    }

    void increaseVariables()
    {
        pillarScript.destroyChance += destroyChanceChange;
        pillarScript.rumbleLength -= rumbleLengthChange;

        managerScript.numOfObjectives += (int)objectiveCountChange;
        managerScript.updateScore(managerScript.score, initialObjectives + (level * objectiveCountChange));
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
