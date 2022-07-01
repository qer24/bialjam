using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SetVCamNearClip : MonoBehaviour
{
    [SerializeField] private float nearClip = -100;
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = nearClip;
    }
}
