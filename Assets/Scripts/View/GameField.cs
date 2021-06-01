using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private float gameWidth = 0;
    [SerializeField] private float gameHeight = 0;
    [SerializeField] private int enemiesRows = 0;
    [SerializeField] private int enemiesCols = 0;
    [SerializeField] private float enemiesSpeed = 0;
    [SerializeField] private float enemiesVerticalSpeed = 0;
    [SerializeField] private MainCharacter player = null;
    [SerializeField] private GameObject enemyPrefab = null;
    private float enemiesMoveDelay = 0.2f;
    private float loseThreshold = 2;

    private Bullet playerBullet;
    private GameObject[,] enemies;
    private Vector3 enemiesMoveDirection;
    private float enemiesCurrentMoveDelay;
    private int currentMovingRow;

    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    private void initialize()
    {
        enemies = new GameObject[enemiesRows, enemiesCols];
        enemiesCurrentMoveDelay = enemiesMoveDelay;
        currentMovingRow = 0;
        enemiesMoveDirection = Vector3.right;
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        handleMoveInput();
        handleShootInput();
        movePlayerBullet();
        updateMoveEnemiesTime();
    }

    private void updateMoveEnemiesTime()
    {
        enemiesCurrentMoveDelay -= Time.deltaTime;
        if (enemiesCurrentMoveDelay <= 0)
        {
            enemiesCurrentMoveDelay = enemiesMoveDelay;
            moveEnemiesRow();
            checkLoseCondition();
            updateEnemiesMoveDirection();
        }
    }

    private void checkLoseCondition()
    {
        if ((enemiesMoveDirection == Vector3.down) && isEnemiesBelowThreshold())
        {
            gameOver();
        }
    }

    private bool isEnemiesBelowThreshold()
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
    }

    private void gameOver()
    {
        cleanUp();
        initialize();
    }

    private void cleanUp()
    {
        removeEnemies();
        destroyBullet();
    }

    private void removeEnemies()
    {
        for (int i = 0; i < enemiesRows; i++)
        {
            for (int j = 0; j < enemiesCols; j++)
            {
                if (enemies[i, j] == null) continue;

                Destroy(enemies[i, j]);
            }
        }
    }

    private void moveEnemiesRow()
    {
        for (int i = 0; i < enemiesCols; i++)
        {
            if (enemies[enemiesRows - currentMovingRow - 1, i] == null) continue;
            enemies[enemiesRows - currentMovingRow - 1, i].transform.position += enemiesMoveDirection * enemiesMoveDelay * getEnemiesSpeed();
        }

        currentMovingRow = (currentMovingRow + 1) % enemiesRows;
    }

    private float getEnemiesSpeed()
    {
        return enemiesMoveDirection == Vector3.down ? enemiesVerticalSpeed : enemiesSpeed;
    }

    private void updateEnemiesMoveDirection()
    {
        if (currentMovingRow == 0)
        {
            enemiesMoveDirection = calculateEnemiesMoveDirection();
        }
    }

    private Vector3 calculateEnemiesMoveDirection()
    {
        Vector3 boundaryVector = findBoundaryVectorByEnemies();
        if (enemiesMoveDirection == Vector3.down)
        {
            return boundaryVector * -1;
        }

        if (boundaryVector == Vector3.left || boundaryVector == Vector3.right)
        {
            return Vector3.down;
        }

        return enemiesMoveDirection;
    }

    private Vector3 getBoundaryVector(GameObject enemy)
    {
        if (enemy.transform.position.x <= -gameWidth)
            return Vector3.left;
        if (enemy.transform.position.x >= gameWidth)
            return Vector3.right;

        return Vector3.zero;
    }

    private Vector3 findBoundaryVectorByEnemies()
    {
        Vector3 result = Vector3.zero;
        for (int i = 0; i < enemiesRows; i++)
        {
            for (int j = 0; j < enemiesCols; j++)
            {
                if (enemies[i, j] == null) continue;

                result = getBoundaryVector(enemies[i, j]);
                if (result != Vector3.zero) {
                    return result;
                }
            }
        }

        return result;
    }

    private void spawnEnemies() 
    {
        GameObject enemy;
        Vector3 position;
        
        for (int i = 0; i < enemiesRows; i++) {
            for (int j = 0; j < enemiesCols; j++)
            {
                enemy = Instantiate(enemyPrefab);
                position = new Vector3(-gameWidth + j * 2.27f, gameHeight - 1 - i * 2.3f, 0);
                enemy.transform.position = position;
                enemies[i, j] = enemy;
            }
        }
    }

    private void handleMoveInput()
    {
        Vector3 newPosition = Vector3.right * GetMovingInput() * player.Speed * Time.deltaTime;
        newPosition += player.transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -gameWidth, gameWidth);
        player.transform.position = newPosition;
    }

    private float GetMovingInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return -1;
        if (Input.GetKey(KeyCode.RightArrow))
            return 1;

        return 0;
    }

    private void movePlayerBullet()
    {
        if (playerBullet == null) return;

        playerBullet.transform.position += Vector3.up * player.BulletSpeed * Time.deltaTime;
        if (playerBullet.transform.position.y >= gameHeight)
        {
            destroyBullet();
        }
    }

    private void destroyBullet()
    {
        if (playerBullet == null) return;
        Destroy(playerBullet.gameObject);
        playerBullet = null;
    }

    private void handleShootInput()
    {
        if (playerBullet != null) return;

        if (Input.GetKey(KeyCode.Space))
        {
            makeShot();
        }
    }

    private void makeShot()
    {
        playerBullet = Instantiate(player.PlayerBulletPrefab).GetComponent<Bullet>();
        playerBullet.name = "PlayerBullet";
        playerBullet.transform.position = player.transform.position;
        playerBullet.registerCollisionCallback(onPlayerBulletCollision);
    }

    private void onPlayerBulletCollision(GameObject gameObject)
    {
        if (gameObject.tag == "Enemy")
        {
            destroyBullet();
            Destroy(gameObject);
        }
    }
}
