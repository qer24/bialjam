using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    void LateUpdate()
    {
        text.text = $"{EnemyManager.instance.deadEnemyCount}/{EnemyManager.instance.startEnemyCount}";
    }
}
