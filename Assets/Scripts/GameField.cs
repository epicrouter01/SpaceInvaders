using UnityEngine;
using System;

public class GameField : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject enemiesContainer = null;
    private readonly float loseThreshold = 2;
    private float timeron = 0;

    // Start is called before the first frame update
    void Start()
    {
        initializeBehaviors();
        initialize();
    }

    private void initializeBehaviors()
    {
        getPlayerShootingBehavior().registerCollisionCallback(onPlayerBulletCollision);
    }

    private void initialize()
    {
        timeron = 0;
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        getEnemiesSpawnBehavior().spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            int row, col;
            row = (int)Mathf.Floor(timeron / enemiesCols);
            col = (int)Mathf.Floor(timeron % enemiesCols);
            timeron = Mathf.Min(timeron + 1, enemiesCols * enemiesRows - 1);
            if (enemies[row, col] != null)
                destroyEnemy(enemies[row, col]);
        }*/
    }

    private void checkLoseCondition()
    {
        /*if ((enemiesMoveDirection == Vector3.down) && isEnemiesBelowThreshold())
        {
            gameOver();
        }*/
    }

    /*private bool isEnemiesBelowThreshold()
    {
        for (int i = 0; i < enemiesRows; i++)
        {
            for (int j = 0; j < enemiesCols; j++)
            {
                if (enemies[i, j] == null) continue;

                if (enemies[i, j].transform.position.y <= -gameHeight + loseThreshold)
                {
                    return true;
                }
            }
        }

        return false;
    }*/

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

    private void onPlayerBulletCollision(GameObject gameObject)
    {
        if (gameObject.tag == "Enemy")
        {
            getPlayerShootingBehavior().destroyBullet();
            destroyEnemy(gameObject);
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
}
