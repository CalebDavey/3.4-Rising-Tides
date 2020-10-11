using UnityEngine.UI;
using UnityEngine;

public class objectiveInteraction : MonoBehaviour
{
<<<<<<< HEAD
    // PUBLIC VARIABLES
    public manager gameManager;
=======
    public manager gameManager;
    public SceneManager sceneManager;

>>>>>>> Objectives
    public int scoreIncrease = 1;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
<<<<<<< HEAD
            gameManager.updateScore(scoreIncrease);
=======
            
            gameManager.updateScore(scoreIncrease, sceneManager.initialObjectives + (sceneManager.level * sceneManager.objectiveCountChange));
>>>>>>> Objectives
            Destroy(this.gameObject);
        }
    }
}
