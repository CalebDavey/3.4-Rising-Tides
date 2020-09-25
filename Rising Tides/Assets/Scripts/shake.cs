using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class shake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineCam;
    private CinemachineBasicMultiChannelPerlin perlin;

    public float shakeMagnitude = 0.1f;
    public bool shaking = false;

    private void Awake()
    {
        cinemachineCam = GetComponent<CinemachineVirtualCamera>();
        perlin = cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0;
    }



    private void Update()
    {
        if (shaking)
        {
            perlin.m_AmplitudeGain = shakeMagnitude;
        }
        else
        {
            perlin.m_AmplitudeGain = 0;
        }
    }
}
