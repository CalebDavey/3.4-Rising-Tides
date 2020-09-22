using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineCam;
    private CinemachineBasicMultiChannelPerlin perlin;

    private float shakeTimer;
    

    private void Awake()
    {
        cinemachineCam = GetComponent<CinemachineVirtualCamera>();
        perlin = cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0;
    }

    public void ShakeCamera(float intensity, float time)
    {
        perlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
        } else
        {
            perlin.m_AmplitudeGain = 0;
        }
    }

}
