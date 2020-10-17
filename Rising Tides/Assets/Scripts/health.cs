using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public int lives = 3;
    public int timeBetweenCalls = 1;


    int totalLives = 3;


    public loadingScenes sceneLoader;
    public Text lifeText;

    float lastCalled = 0;

    private void Start()
    {
        totalLives = lives;
    }

    private void Update()
    {
        lifeText.text = "Lives: " + lives + " / " + totalLives;
    }
    public void loseLife()
    {
        if (Time.time - lastCalled >= timeBetweenCalls)
        {
            if (lives > 1)
            {
                lives--;
            }
            else
            {
                sceneLoader.fail();
            }
            lastCalled = Time.time;
        }
    }
}
