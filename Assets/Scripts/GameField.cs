using UnityEngine;
using System;

public class GameField : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject enemiesContainer = null;
    [SerializeField] private GameObject playerLifesContainer = null;
    [SerializeField] private GameObject protectorsContainer = null;

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
        if (target.tag == "Protector")
        {
            getEnemiesShootingBehavior().destroyBullet(bullet);
            target.GetComponent<ProtectorBehavior>().Lifes -= 1;
            getProtectorsManager().updateProtectors();
        }
    }

    private void initialize()
    {
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        getEnemiesSpawnBehavior().spawnEnemies();
        getEnemiesMovingBehavior().resetAll();
        getPlayerLifesBehavior().cleanUp();
        getProtectorsManager().setUp();
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
        if (target.tag == "Protector")
        {
            getPlayerShootingBehavior().destroyBullet();
        }
    }

    private void destroyEnemy(GameObject enemy)
    {
        getEnemiesDestroyerBehavior().destroyEnemy(enemy.GetComponent<EnemyBehavior>());
        getEnemiesMovingBehavior().onEnemyDestroyed();
    }

    private InputShootingBehavior getPlayerShootingBehavior()
    {
        return player.GetComponent<InputShootingBehavior>();
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
    private EnemiesDestroyerBehavior getEnemiesDestroyerBehavior()
    {
        return enemiesContainer.GetComponent<EnemiesDestroyerBehavior>();
    }
    private LifesBehavior getPlayerLifesBehavior()
    {
        return playerLifesContainer.GetComponent<LifesBehavior>();
    }
    private ProtectorManagerBehavior getProtectorsManager()
    {
        return protectorsContainer.GetComponent<ProtectorManagerBehavior>();
    }
}
