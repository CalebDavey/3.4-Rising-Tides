using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextLevel : MonoBehaviour
{
    public levelManager sceneManager;
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            sceneManager.progressLevels();
        }
    }
}
