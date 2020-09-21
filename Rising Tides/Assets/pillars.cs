using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillar1 : MonoBehaviour
{
    float height = 5f;
    public float time = 1f;
    public float heightRandomisation = 5f;

    void Start()
    {
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine("pillarRise");
        }
    }
}
