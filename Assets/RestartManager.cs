using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartManager : Singleton<RestartManager>
{
    [SerializeField] private EventReference restartSfx;
    
    private FirstPersonController player;
    private Vector3 startPos;

    public override void Awake()
    {
        keepAlive = false;
        base.Awake();
        
        player = GameObject.FindWithTag("Player").GetComponentInChildren<FirstPersonController>();
        startPos = player.transform.position;
    }

    private void Update()
    {
        switch (Keyboard.current.leftCtrlKey.isPressed)
        {
            case true when Keyboard.current.digit1Key.wasPressedThisFrame:
                SceneManager.LoadScene(0);
                break;
            case true when Keyboard.current.digit2Key.wasPressedThisFrame:
                SceneManager.LoadScene(1);
                break;
            case true when Keyboard.current.digit3Key.wasPressedThisFrame:
                SceneManager.LoadScene(2);
                break;
            case true when Keyboard.current.digit4Key.wasPressedThisFrame:
                SceneManager.LoadScene(3);
                break;
            case true when Keyboard.current.digit5Key.wasPressedThisFrame:
                SceneManager.LoadScene(4);
                break;
            case true when Keyboard.current.digit6Key.wasPressedThisFrame:
                SceneManager.LoadScene(5);
                break;
        }
    }                                                                                                                        

    public void Restart()
    {
        restartSfx.Play();
        
        player.gameObject.SetActive(true);
        
        player.TeleportPlayer(startPos, Quaternion.identity);
        player.GetComponentInParent<PlayerSizeManager>().Reset();
        EnemyManager.instance.Reset();
        TimerManager.instance.Reset();
        
        gameObject.SetActive(false);
    }
}