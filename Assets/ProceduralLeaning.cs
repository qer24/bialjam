using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ProceduralLeaning : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs input; 
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float leanAmountX, leanAmountZ;
    
    private Quaternion currentLean;
    
    private void Update()
    {
        Quaternion leanX = Quaternion.AngleAxis(leanAmountX * Mathf.Clamp01(input.move.y), Vector3.right);
        Quaternion leanZ = Quaternion.AngleAxis(leanAmountZ * -input.move.x, Vector3.forward);
        
        currentLean = Quaternion.Slerp(currentLean, leanX * leanZ, lerpSpeed * Time.deltaTime);

        transform.localRotation = currentLean;
    }
}
