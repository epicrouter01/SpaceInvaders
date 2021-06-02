using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameField : MonoBehaviour
{
    [SerializeField] private float gameWidth = 0;
    [SerializeField] private float gameHeight = 0;
    [SerializeField] private int enemiesRows = 0;
    [SerializeField] private int enemiesCols = 0;
    [SerializeField] private float enemiesInitialSpeed = 0;
    [SerializeField] private float enemiesMaxSpeed = 0;
    [SerializeField] private float enemiesVerticalMove = 0;
    [SerializeField] private MainCharacter player = null;
    [SerializeField] private GameObject playerBulletPrefab = null;
    [SerializeField] private GameObject enemyPrefab = null;
    private readonly float enemiesInitialMoveDelay = 0.2f;
    private readonly float loseThreshold = 2;
    private readonly float playerSpeed = 12;
    private readonly float bulletSpeed = 30;

    private HorizontalMovementBehavior playerMovementBehavior;
    private Bullet playerBullet;
    private GameObject[,] enemies;
    private Vector3 enemiesMoveDirection;
    private float enemiesSpeed;
    private float enemiesMoveDelay;
    private int currentMovingRow;

    private float timeron = 0;

    // Start is called before the first frame update
    void Start()
    {
        setBehaviors();
        initialize();
    }

    private void setBehaviors()
    {
        playerMovementBehavior = new HorizontalMovementBehavior(player.gameObject, gameWidth, playerSpeed);
    }

    private void initialize()
    {
        timeron = 0;
        enemies = new GameObject[enemiesRows, enemiesCols];
        enemiesMoveDelay = enemiesInitialMoveDelay;
        enemiesSpeed = enemiesInitialSpeed;
        currentMovingRow = enemiesRows - 1;
        enemiesMoveDirection = Vector3.right;
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        handleShootInput();
        movePlayerBullet();
        moveEnemies();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            int row, col;
            row = (int)Mathf.Floor(timeron / enemiesCols);
            col = (int)Mathf.Floor(timeron % enemiesCols);
            timeron = Mathf.Min(timeron + 1, enemiesCols * enemiesRows - 1);
            if (enemies[row, col] != null)
                destroyEnemy(enemies[row, col]);
        }
    }

    private void moveEnemies()
    {
        enemiesMoveDelay -= Time.deltaTime;
        if (enemiesMoveDelay <= 0)
        {
            calculateEnemiesMovementDelay();
            moveEnemiesRow();
            updateCurrentMovingRow();
            updateEnemiesMoveDirection();
            checkLoseCondition();
        }
    }

    private void updateCurrentMovingRow()
    {
        currentMovingRow = findClosestNonEmptyRow(currentMovingRow);
    }

    private void calculateEnemiesMovementDelay()
    {
        enemiesMoveDelay = enemiesInitialMoveDelay * (enemiesInitialSpeed / enemiesSpeed);
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
            if (enemies[currentMovingRow, i] == null) continue;
            enemies[currentMovingRow, i].transform.position += getEnemiesMoveVector();
        }

        currentMovingRow -= 1;
        if (currentMovingRow < 0)
            currentMovingRow = enemiesRows - 1;
    }

    private Vector3 getEnemiesMoveVector()
    {
        Vector3 result;

        if (enemiesMoveDirection == Vector3.down)
            result = new Vector3(0, -enemiesVerticalMove, 0);
        else
            result = enemiesMoveDirection * enemiesMoveDelay * enemiesSpeed;

        return result;
    }

    private void updateEnemiesMoveDirection()
    {
        if (currentMovingRow == getLowestRow())
        {
            enemiesMoveDirection = calculateEnemiesMoveDirection();
        }
    }

    private int findClosestNonEmptyRow(int row)
    {
        for (int i = row; i >= 0; i--)
            if (isRawHasEnemies(i))
                return i;

        for (int i = enemiesRows - 1; i > row; i--)
            if (isRawHasEnemies(i))
                return i;

        return 0;
    }

    private int getLowestRow()
    {
        for (int i = enemiesRows - 1; i >= 0; i--)
            if (isRawHasEnemies(i))
                return i;
        return 0;
    }

    private bool isRawHasEnemies(int row)
    {
        for (int j = 0; j < enemiesCols; j++)
            if (enemies[row, j] != null)
                return true;

        return false;
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

    private void movePlayer()
    {
        playerMovementBehavior.update(Time.deltaTime);
    }

    private void movePlayerBullet()
    {
        if (playerBullet == null) return;

        playerBullet.transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
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
        playerBullet = Instantiate(playerBulletPrefab).GetComponent<Bullet>();
        playerBullet.name = "PlayerBullet";
        playerBullet.transform.position = player.transform.position;
        playerBullet.registerCollisionCallback(onPlayerBulletCollision);
    }

    private void onPlayerBulletCollision(GameObject gameObject)
    {
        if (gameObject.tag == "Enemy")
        {
            destroyBullet();
            destroyEnemy(gameObject);
        }
    }

    private void destroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
        calculateEnemiesSpeed();
        updateCurrentMovingRow();
    }

    private void calculateEnemiesSpeed()
    {
        float ratio = (float)getEnemiesCount() / (float)(enemiesRows * enemiesCols);
        enemiesSpeed = enemiesMaxSpeed - (enemiesMaxSpeed - enemiesInitialSpeed) * ratio;
    }

    private int getEnemiesCount()
    {
        int result = 0;
        for (int i = 0; i < enemiesRows; i++)
            for (int j = 0; j < enemiesCols; j++)
            {
                if (enemies[i, j] != null)
                    result++;
            }

        return result;
    }
}
