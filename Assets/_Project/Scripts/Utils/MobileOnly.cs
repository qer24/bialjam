using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnly : MonoBehaviour
{
    #if !UNITY_ANDROID && !UNITY_IOS
    private void OnEnable()
    {
        gameObject.SetActive(false);
    }
    #endif
}