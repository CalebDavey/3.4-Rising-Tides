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
    public float objectiveCountChange = 0.5f;

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
            playerScript.animator.SetBool("Running", false);
            playerScript.animator.SetBool("Jumping", false);

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
    }
}
