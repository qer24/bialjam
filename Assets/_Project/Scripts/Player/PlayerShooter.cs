using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using StarterAssets;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private PlayerInnacuracy inaccuracy;

    [Space] 
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private PlayerInnacuracy.InaccuracySource shootInaccuracy;

    [Space] 
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private AutoTween bulletHoleDissapearTween;
    [SerializeField] private LineRenderer tracerPrefab;
    [SerializeField] private AutoTween tracerTween;

    [Space] 
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeAmplitude = 3f;
    [SerializeField] private float shakeFrequency = 1f;

    [Space] 
    [SerializeField] private float killKnockbackForce;

    private float cooldownTimer = -0.1f;

    private void Awake()
    {
        inaccuracy.inaccuracySources.Add(shootInaccuracy);
        bulletHoleDissapearTween.autoCancel = false;
        tracerTween.autoCancel = false;
    }

    private void OnEnable()
    {
        input.onShoot += Shoot;
    }

    private void OnDisable()
    {
        input.onShoot -= Shoot;
    }

    private void Update()
    {
        if (cooldownTimer > 0f) cooldownTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (cooldownTimer > 0f) return;
        
        cooldownTimer = shootCooldown;
        shootInaccuracy.SetMax();
        
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out var hit, 100f))
        {
            BulletHole(hit);
            Tracer(hit);
            CameraShake.instance.Shake(shakeDuration, shakeAmplitude, shakeFrequency);

            if (hit.collider.transform.parent.TryGetComponent(out Health health))
            {
                health.RemoveHealth(1);

                if (health.isDead)
                {
                    health.GetComponentInChildren<Rigidbody>().AddExplosionForce(killKnockbackForce, hit.point + hit.normal * 0.2f, 0.25f, 0.5f, ForceMode.Impulse);
                }
            }
        }
    }

    private void BulletHole(RaycastHit hit)
    {
        var bulletHole = LeanPool.Spawn(bulletHolePrefab, hit.point + hit.normal * 0.1f, Quaternion.identity, hit.collider.transform);
        bulletHole.transform.localScale = bulletHolePrefab.transform.localScale;
        bulletHoleDissapearTween.Play(bulletHole.transform.LeanScale(Vector3.zero, bulletHoleDissapearTween.time));
        LeanPool.Despawn(bulletHole, bulletHoleDissapearTween.time + 0.25f);
    }

    private void Tracer(RaycastHit hit)
    {
        var tracer = LeanPool.Spawn(tracerPrefab);
        tracer.SetPosition(0, playerCam.transform.position + Vector3.down);
        tracer.SetPosition(1, hit.point);

        tracerTween.Play(LeanTween.value(gameObject, val =>
        {
            tracer.SetPosition(0, Vector3.Lerp(playerCam.transform.position + Vector3.down, hit.point, val));
        }, 0f, 1f, tracerTween.time));
        
        LeanPool.Despawn(tracer, tracerTween.time + 0.25f);
    }
}
