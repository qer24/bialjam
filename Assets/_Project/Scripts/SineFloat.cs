using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SineFloat : MonoBehaviour
{
    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private float sineMagnitude = 0.25f;
    [SerializeField] private bool randomTimeOffset = true;

    private Vector3 startPos;
    private float timeOffset = 0;
    private float speed;

    private void Awake()
    {
        startPos = transform.localPosition;
        if (randomTimeOffset) timeOffset = new Squirrel3().Range(1f, 99f);
        speed = new Squirrel3().Range(minMaxSpeed.x, minMaxSpeed.y);
    }

    private void Update()
    {
        transform.localPosition = startPos + (Vector3.up * Mathf.Sin((Time.time + timeOffset) * speed) * sineMagnitude);
    }
}