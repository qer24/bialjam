using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private class EnemyRef
    {
        public GameObject gameObject;
        public Health hp;

        public Transform transform => gameObject.transform;

        public EnemyRef(GameObject gameObject, Health hp)
        {
            this.gameObject = gameObject;
            this.hp = hp;
        }
    }
    
    private EnemyRef[] enemies;
    private EnemyRef[] enemyCopies;

    private bool levelFinished = false;

    public int startEnemyCount;
    public int deadEnemyCount;
    
    public override void Awake()
    {
        keepAlive = false;
        base.Awake();

        var enemyGameobjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new EnemyRef[enemyGameobjects.Length];
        for (var i = 0; i < enemyGameobjects.Length; i++)
        {
            var enemyGo = enemyGameobjects[i];
            enemies[i] = new EnemyRef(enemyGo, enemyGo.GetComponent<Health>());
        }

        SpawnEnemyCopies();
        startEnemyCount = enemies.Length;
    }

    private void Update()
    {
        if (levelFinished) return;

        deadEnemyCount = enemies.Count(enemy => enemy.hp.isDead);
        if (deadEnemyCount < startEnemyCount) return;
        
        levelFinished = true;
        TimerManager.instance.Stop();
    }

    private void SpawnEnemyCopies()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        enemyCopies = new EnemyRef[enemies.Length];
        for (var i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i];
            var copyGo = Instantiate(enemy.gameObject, enemy.transform.position, enemy.transform.rotation, transform);
            copyGo.SetActive(false);

            enemyCopies[i] = new EnemyRef(copyGo, copyGo.GetComponent<Health>());
        }
    }

    public void Reset()
    {
        levelFinished = false;
        
        for (var i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                Destroy(enemies[i].gameObject);
            }
            
            enemyCopies[i].gameObject.SetActive(true);
            enemyCopies[i].transform.SetParent(null);
                
            enemies[i] = enemyCopies[i];
        }
        
        SpawnEnemyCopies();
    }
}
