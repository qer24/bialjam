using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Lean.Pool;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    [SerializeField] private float detectionTime = 0.5f;
    [SerializeField] private float detectionRange = 5f;

    [Space]
    [SerializeField] private EventReference detectionSfx;
    [SerializeField] private GameObject exclamation;
    [SerializeField] private float exclamationTime = 0.5f;

    [Space]
    [SerializeField] private float shootCoolddown = 0.5f;
    [SerializeField] private Transform[] shootPoints;
    [SerializeField] private EventReference shootSfx;
    [SerializeField] private AutoTween scaleDownTween;
    [SerializeField] private Vector3 shootingScale;

    [Space]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletForce = 20f;
    
    private Health health;
    private Transform playerCamera;
    private CharacterController playerController;

    private float detectionTimer = 0f;
    private bool hasDetected;

    private Vector3 playerCamPos => playerCamera.transform.position;
    
    private Transform gfx;

    private int currentShot;
    private float shootTimer;

    private Vector3 startScale;

    private List<Bullet> aliveBullets = new List<Bullet>();
    
    private void Awake()
    {
        var playerGo = GameObject.FindWithTag("Player");
        playerCamera = playerGo.GetComponentInChildren<Camera>().transform;
        playerController = playerGo.GetComponentInChildren<CharacterController>();
        health = GetComponent<Health>();

        gfx = transform.GetChild(0);

        startScale = transform.localScale;
    }

    private void Update()
    {
        if (health.isDead) return;
        
        if (!hasDetected)
        {
            var rayDir = (playerCamPos - transform.position).normalized;

            if (Physics.Raycast(transform.position, rayDir, out var hit, detectionRange))
            {
                if (hit.collider == playerController)
                {
                    detectionTimer += Time.deltaTime;
                }
            }
            else
            {
                detectionTimer = 0f;
            }

            if (detectionTimer >= detectionTime)
            {
                detectionSfx.Play(transform.position);
                
                hasDetected = true;
                anim.enabled = true;
                
                exclamation.SetActive(true);
                WaitUtils.Wait(exclamationTime, true, () => exclamation.SetActive(false));

                shootTimer = shootCoolddown * 0.5f;

                ShootScale();
            }
        }
        else
        {
            transform.LookAt(playerCamPos);
            gfx.localRotation = Quaternion.identity;

            shootTimer += Time.deltaTime;

            if (shootTimer > shootCoolddown)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    private void ShootScale()
    {
        transform.localScale = shootingScale;
        scaleDownTween.Play(transform.LeanScale(startScale, scaleDownTween.time));
    }

    private void OnEnable()
    {
        aliveBullets = new List<Bullet>();
    }

    private void Shoot()
    {
        var currentShootPoint = shootPoints[currentShot % 2];
        shootSfx.Play(currentShootPoint.position);
        ShootScale();

        var bulletDir = (playerCamera.position - currentShootPoint.position).normalized;
        var bullet = LeanPool.Spawn(bulletPrefab, currentShootPoint.position, Quaternion.identity);
        bullet.rb.AddForce(bulletDir * bulletForce, ForceMode.Impulse);

        aliveBullets.Add(bullet);
        
        WaitUtils.Wait(5f, true,
            () => {
                if (bullet == null) return;
                if (!bullet.gameObject.activeSelf) return;

                LeanPool.Despawn(bullet.gameObject);
                aliveBullets.Remove(bullet);
            });

        currentShot++;
    }

    private void OnDisable()
    {
        foreach (var bullet in aliveBullets)
        {
            if (bullet == null) return;
            if (!bullet.gameObject.activeSelf) return;
            
            LeanPool.Despawn(bullet.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}