using UnityEngine;
using System;

public class GameField : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject enemiesContainer = null;
    [SerializeField] private GameObject playerLifesContainer = null;
    [SerializeField] private GameObject protectorsContainer = null;
    [SerializeField] private GameObject scoreContainer = null;

    private Action<int> onGameOverCallback;

    // Start is called before the first frame update
    void Start()
    {
        initializeBehaviors();
        restartGame();
    }

    public void restartGame()
    {
        resumeGame();
        startGame();
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

    private void startGame()
    {
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        getEnemiesSpawnBehavior().spawnEnemies();
        getEnemiesMovingBehavior().onGameStarted();
        getPlayerLifesBehavior().onGameStarted();
        getProtectorsManager().onGameStarted();
        getScoreChangerBehavior().onGameStarted();
    }

    public void registerGameOverCallback(Action<int> callback)
    {
        onGameOverCallback = callback;
    }

    private void gameOver()
    {
        cleanUp();
        pauseGame();
        makeGameoverCallback();
    }

    private void resumeGame()
    {
        Time.timeScale = 1;
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
    }

    private void makeGameoverCallback()
    {
        if (onGameOverCallback != null)
            onGameOverCallback(getScoreChangerBehavior().Score);
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
        int count = getEnemiesDestroyerBehavior().destroyEnemy(enemy.GetComponent<EnemyBehavior>());
        getEnemiesMovingBehavior().onEnemyDestroyed();
        getScoreChangerBehavior().onEnemiesDestroyed(count);
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
    private ScoreChangerBehavior getScoreChangerBehavior()
    {
        return scoreContainer.GetComponent<ScoreChangerBehavior>();
    }
}
