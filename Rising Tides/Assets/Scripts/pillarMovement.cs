using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarMovement : MonoBehaviour
{
    public float time = 1f;
    public float height;
    public float destroyChance = 5;
    public float rumbleLength = 3;
    public float rumbleMagnitude = 0.05f;
    public GameObject player;

    Vector3 startPosition;

    float chance;

    private void Start()
    {
        chance = Random.Range(1, 100);
        startPosition = transform.position;
        StartCoroutine(pillarRise(height, time));
    }

    void triggerPillarDestroy()
    {
        if (chance <= destroyChance)
        {
            StartCoroutine(pillarRumble(rumbleLength, rumbleMagnitude));
        }
    }

    /********************************* 
     PILLAR RISE 
    *********************************/
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

    /********************************* 
     PILLAR RUMBLE 
    *********************************/
    public IEnumerator pillarRumble(float rumbleTime, float mag)
    {
        float StartTime = Time.time;
        float RumbleEndTime = StartTime + rumbleTime;

        Vector3 startPos = this.transform.position;

        while (Time.time < RumbleEndTime)
        {
            Vector3 pos = startPos;
            pos.y += Random.Range(-mag, mag);
           // pos.z += Random.Range(-mag, mag);

            this.transform.position = pos;

            yield return new WaitForFixedUpdate();
        }
        this.transform.position = startPos;
        StartCoroutine(pillarDescend(startPosition.y, time, true));
    }

    /********************************* 
     PILLAR DESCENT 
    *********************************/
    public IEnumerator pillarDescend( float endHeight, float descentTime, bool destroy)
    {
        float StartTime = Time.time;
        float DescendEndTime = StartTime + descentTime;

        Vector3 startPos = this.transform.position;
        Vector3 endPos = new Vector3(this.transform.position.x, endHeight, this.transform.position.z);

            while (Time.time < DescendEndTime)
        {
            float timeProgressed = (Time.time - StartTime) / descentTime;  // this will be 0 at the beginning and 1 at the end.
            this.transform.position = Vector3.Lerp(startPos, endPos, timeProgressed);

            yield return new WaitForFixedUpdate();
        }
        if(destroy)
        {
            Destroy(this.gameObject);
        }
    }
}
