using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    [SerializeField] private AutoTween shakeTween;
    
    private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;

    public override void Awake()
    {
        keepAlive = false;
        base.Awake();

        cam = GetComponent<CinemachineVirtualCamera>();
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float duration, float amplitude, float frequency = 1f)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
        
        shakeTween.Play(LeanTween.value(gameObject, val =>
        {
            noise.m_AmplitudeGain = val;
        }, amplitude, 0f, duration));
    }
}
