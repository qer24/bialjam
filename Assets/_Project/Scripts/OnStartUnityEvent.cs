using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStartUnityEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent Event;
    
    // Start is called before the first frame update
    void Start()
    {
        Event?.Invoke();
    }
}
