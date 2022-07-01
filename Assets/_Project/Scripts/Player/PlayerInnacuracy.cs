using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInnacuracy : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private InaccuracySource movingInacuracy;
    
    public float inaccuracy = 0;

    public List<InaccuracySource> inaccuracySources = new List<InaccuracySource>();

    [Serializable]
    public class InaccuracySource
    {
        [SerializeField] private float max;
        [SerializeField] private float increaseSpeed;
        [SerializeField] private float decreaseSpeed;
        
        public float Current { get; set; }
        public void Decrease(float deltaTime)
        {
            Current -= deltaTime * decreaseSpeed;
            Current = Mathf.Max(Current, 0);
        }

        public void Increase(float deltaTime)
        {
            Current += deltaTime * increaseSpeed;
            Current = Mathf.Min(Current, max);
        }

        public void SetMax() => Current = max;
    }

    private void Awake()
    {
        inaccuracySources.Add(movingInacuracy);
    }

    private void Update()
    {
        DecreaseAllSources();
        
        if (characterController.velocity.magnitude > 0.5f)
        {
            movingInacuracy.Increase(Time.deltaTime);
        }

        inaccuracy = inaccuracySources.Sum(source => source.Current);
    }

    private void DecreaseAllSources()
    {
        foreach (var source in inaccuracySources)
        {
            source.Decrease(Time.deltaTime);
        }
    }
}
