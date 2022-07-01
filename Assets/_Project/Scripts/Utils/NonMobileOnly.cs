using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMobileOnly : MonoBehaviour
{
    #if UNITY_ANDROID || UNITY_IOS
    private void OnEnable()
    {
        gameObject.SetActive(false);
    }
    #endif
}