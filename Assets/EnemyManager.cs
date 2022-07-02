using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private GameObject[] enemies;
    private GameObject[] enemyCopies;

    public override void Awake()
    {
        keepAlive = false;
        base.Awake();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        SpawnEnemyCopies();
    }

    private void SpawnEnemyCopies()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        enemyCopies = new GameObject[enemies.Length];
        for (var i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i];
            enemyCopies[i] = Instantiate(enemy, enemy.transform.position, enemy.transform.rotation, transform);
            enemyCopies[i].SetActive(false);
        }
    }

    public void Reset()
    {
        for (var i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                Destroy(enemies[i]);
            }
            
            enemyCopies[i].SetActive(true);
            enemyCopies[i].transform.SetParent(null);
                
            enemies[i] = enemyCopies[i];
        }
        
        SpawnEnemyCopies();
    }
}
