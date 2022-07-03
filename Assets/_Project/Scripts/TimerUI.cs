using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    void LateUpdate()
    {
        text.text = TimerManager.instance.timer.ToTime("00:00.000");
    }
}
