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
        CreatePillar();
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine("pillarRise");
        }
    }
}

public void CreatePillar()
{
    float offset = Random.Range(1, heightRandomisation);

    // HEIGHTS DETERMINED BY PERLIN NOISE
    height = Mathf.Round(Mathf.PerlinNoise((this.transform.position.x + offset) / 10, (this.transform.position.y + offset) / 10) * 10);
}


IEnumerator pillarRise()
{
    float StartTime = Time.time;
    float EndTime = StartTime + time;

    while (Time.time < EndTime)
    {
        float timeProgressed = (Time.time - StartTime) / time;  // this will be 0 at the beginning and 1 at the end.
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, height, this.transform.position.z), timeProgressed);

        yield return new WaitForFixedUpdate();
    }
}
}
