﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarMovement : MonoBehaviour
{
    public float time = 1f;

    public float height;

    private void Start()
    {
            StartCoroutine(pillarRise(height, time));
    }


    public IEnumerator pillarRise(float riseHeight, float riseTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + riseTime;

        Vector3 startPos = this.transform.position;
        Vector3 endPos = new Vector3(this.transform.position.x, riseHeight, this.transform.position.z);
        
            while (Time.time < EndTime)
            {
                float timeProgressed = (Time.time - StartTime) / riseTime;  // this will be 0 at the beginning and 1 at the end.
                this.transform.position = Vector3.Lerp(startPos, endPos, timeProgressed);

                yield return new WaitForFixedUpdate();
            }
    }
}
