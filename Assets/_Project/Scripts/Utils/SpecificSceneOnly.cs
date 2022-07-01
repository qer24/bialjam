using System;
using System.Collections;
using System.Collections.Generic;
using Tymski;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecificSceneOnly : MonoBehaviour
{
    [SerializeField] private SceneReference scene;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().path != scene.ScenePath)
        {
            gameObject.SetActive(false);
        }
    }
}