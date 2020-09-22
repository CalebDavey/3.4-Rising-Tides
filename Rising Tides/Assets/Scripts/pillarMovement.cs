using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarMovement : MonoBehaviour
{
    public float pillarNoiseOffset = 5;
    public float time = 1f;
    public float shakeMagnitude = 0.05f;

    float height;

    public void Start()
        {
            float pillarOffset = Random.Range(0, pillarNoiseOffset);
            
            // height = Random.Range(5, 10); // RANDOM HEIGHTS
            height = Mathf.Round(Mathf.PerlinNoise((this.transform.position.x + pillarOffset) / 10, (this.transform.position.y + pillarOffset) / 10) * 10);
        }
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                StartCoroutine(pillarRise(height, time));
            }
        }


        public IEnumerator pillarRise(float riseHeight, float riseTime)
        {
            float StartTime = Time.time;
            float EndTime = StartTime + riseTime;

            while (Time.time < EndTime)
            {
                float timeProgressed = (Time.time - StartTime) / riseTime;  // this will be 0 at the beginning and 1 at the end.
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, riseHeight, this.transform.position.z), timeProgressed);


                yield return new WaitForFixedUpdate();
            }
        }
    }
