﻿using UnityEngine.UI;
using UnityEngine;

public class objectiveInteraction : MonoBehaviour
{
    public manager gameManager;
    public levelManager sceneManager;
    public int scoreIncrease = 1;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            gameManager.updateScore(scoreIncrease);
            Destroy(this.gameObject);
        }
    }
}
