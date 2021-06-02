﻿using UnityEngine;
using System;

public class GameField : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject enemiesContainer = null;
    [SerializeField] private GameObject playerLifesContainer = null;

    // Start is called before the first frame update
    void Start()
    {
        initializeBehaviors();
        initialize();
    }

    private void initializeBehaviors()
    {
        getPlayerShootingBehavior().registerCollisionCallback(onPlayerBulletCollision);
        getEnemiesMovingBehavior().registerGameOverCallback(gameOver);
        getEnemiesShootingBehavior().registerCollisionCallback(onEnemyBulletCollision);
    }

    private void onEnemyBulletCollision(GameObject target, GameObject bullet)
    {
        if (target.tag == "Player")
        {
            getEnemiesShootingBehavior().destroyBullet(bullet);
            getPlayerLifesBehavior().LifesCount -= 1;
            if (getPlayerLifesBehavior().LifesCount <= 0)
                gameOver();
        }
    }

    private void initialize()
    {
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        getEnemiesSpawnBehavior().spawnEnemies();
        getEnemiesMovingBehavior().resetAll();
        getPlayerLifesBehavior().cleanUp();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void gameOver()
    {
        cleanUp();
        initialize();
    }

    private void cleanUp()
    {
        getEnemiesSpawnBehavior().removeEnemies();
        getPlayerShootingBehavior().destroyBullet();
    }

    private void onPlayerBulletCollision(GameObject target, GameObject bullet)
    {
        if (target.tag == "Enemy")
        {
            getPlayerShootingBehavior().destroyBullet();
            destroyEnemy(target);
        }
    }

    private void destroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
        getEnemiesMovingBehavior().onEnemyDestroyed();
    }

    private InputShootingBehavior getPlayerShootingBehavior()
    {
        return player.GetComponent<InputShootingBehavior>();
    }
    private HorizontalMovementBehavior getPlayerMovementBehavior()
    {
        return player.GetComponent<HorizontalMovementBehavior>();
    }

    private EnemiesSpawnBehavior getEnemiesSpawnBehavior()
    {
        return enemiesContainer.GetComponent<EnemiesSpawnBehavior>();
    }
    private EnemiesMovingBehavior getEnemiesMovingBehavior()
    {
        return enemiesContainer.GetComponent<EnemiesMovingBehavior>();
    }
    private EnemiesShootingBehavior getEnemiesShootingBehavior()
    {
        return enemiesContainer.GetComponent<EnemiesShootingBehavior>();
    }
    private LifesBehavior getPlayerLifesBehavior()
    {
        return playerLifesContainer.GetComponent<LifesBehavior>();
    }
}
