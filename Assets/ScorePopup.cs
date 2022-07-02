using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private AutoTween tween;

    [Button]
    public void Popup(int amount)
    {
        GetComponent<TextMeshPro>().text = amount.ToString();
        
        var startPos = transform.position;
        var startScale = transform.localScale;
        
        tween.Play(LeanTween.value(gameObject, val =>
        {
            transform.position = startPos + Vector3.up * movementCurve.Evaluate(val);
            transform.localScale = startScale * Mathf.Lerp(1f, 0f, val);
        }, 0, 1f, tween.time));
        
        LeanPool.Despawn(gameObject, tween.time + 0.1f);
    }
}
