using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private ScorePopup popupPrefab;

    public void Score()
    {
        ScoreManager.instance.AddScore(amount);
        var popup = LeanPool.Spawn(popupPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        popup.Popup(amount);
    }
}
