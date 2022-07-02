using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int currentScore = 0;
    public event Action OnScoreChanged; 

    public override void Awake()
    {
        keepAlive = false;
        base.Awake();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        OnScoreChanged?.Invoke();
    }
}
