using System;
using UnityEngine;

public class EnemiesMovingBehavior : MonoBehaviour
{
    [SerializeField] private float enemiesInitialSpeed = 0;
    [SerializeField] private float enemiesMaxSpeed = 0;
    [SerializeField] private float enemiesVerticalMove = 0;

    private readonly float enemiesInitialMoveDelay = 0.2f;
    private readonly float loseThreshold = 2;

    private Action onGameOver;
    private Vector3 enemiesMoveDirection;
    private float enemiesSpeed;
    private float enemiesMoveDelay;
    private int currentMovingRow;

    public float EnemiesInitialSpeed { get => enemiesInitialSpeed; set => enemiesInitialSpeed = value; }
    public float EnemiesMaxSpeed { get => enemiesMaxSpeed; set => enemiesMaxSpeed = value; }
    public float EnemiesVerticalMove { get => enemiesVerticalMove; set => enemiesVerticalMove = value; }

    private void initialize()
    {
        enemiesMoveDelay = enemiesInitialMoveDelay;
        enemiesSpeed = enemiesInitialSpeed;
        currentMovingRow = getSpawner().EnemiesRows - 1;
        enemiesMoveDirection = Vector3.right;
    }

    public void onGameStarted()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemies();
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

    public void registerGameOverCallback(Action callback)
    {
        onGameOver = callback;
    }

    private void checkLoseCondition()
    {
        if ((enemiesMoveDirection == Vector3.down) && isEnemiesBelowThreshold())
        {
            gameOver();
        }
    }

    private void gameOver()
    {
        if (onGameOver != null)
            onGameOver();
    }

    private bool isEnemiesBelowThreshold()
    {
        for (int i = 0; i < getSpawner().EnemiesRows; i++)
            for (int j = 0; j < getSpawner().EnemiesCols; j++)
            {
                if (getEnemies()[i, j] == null) continue;

                if (getEnemies()[i, j].transform.position.y <= -getGameWorld().GameHeight + loseThreshold)
                {
                    return true;
                }
            }

        return false;
    }

    private void calculateEnemiesMovementDelay()
    {
        enemiesMoveDelay = enemiesInitialMoveDelay * (enemiesInitialSpeed / enemiesSpeed);
    }

    private void moveEnemiesRow()
    {
        for (int i = 0; i < getSpawner().EnemiesCols; i++)
        {
            if (getEnemies()[currentMovingRow, i] == null) continue;
            getEnemies()[currentMovingRow, i].transform.position += getEnemiesMoveVector();
            getEnemies()[currentMovingRow, i].GetComponent<EnemyBehavior>().flick();
        }

        currentMovingRow -= 1;
        if (currentMovingRow < 0)
            currentMovingRow = getSpawner().EnemiesRows - 1;
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

    private void updateCurrentMovingRow()
    {
        currentMovingRow = findClosestNonEmptyRow(currentMovingRow);
    }

    private int findClosestNonEmptyRow(int row)
    {
        for (int i = row; i >= 0; i--)
            if (isRawHasEnemies(i))
                return i;

        for (int i = getSpawner().EnemiesRows - 1; i > row; i--)
            if (isRawHasEnemies(i))
                return i;

        return 0;
    }

    private bool isRawHasEnemies(int row)
    {
        for (int j = 0; j < getSpawner().EnemiesCols; j++)
            if (getEnemies()[row, j] != null)
                return true;

        return false;
    }

    private void updateEnemiesMoveDirection()
    {
        if (currentMovingRow == getLowestRow())
        {
            enemiesMoveDirection = calculateEnemiesMoveDirection();
        }
    }

    private int getLowestRow()
    {
        for (int i = getSpawner().EnemiesRows - 1; i >= 0; i--)
            if (isRawHasEnemies(i))
                return i;
        return 0;
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

    private Vector3 findBoundaryVectorByEnemies()
    {
        Vector3 result = Vector3.zero;
        for (int i = 0; i < getSpawner().EnemiesRows; i++)
        {
            for (int j = 0; j < getSpawner().EnemiesCols; j++)
            {
                if (getEnemies()[i, j] == null) continue;

                result = getBoundaryVector(getEnemies()[i, j]);
                if (result != Vector3.zero)
                {
                    return result;
                }
            }
        }

        return result;
    }

    private Vector3 getBoundaryVector(GameObject enemy)
    {
        if (enemy.transform.position.x <= -getGameWorld().GameWidth)
            return Vector3.left;
        if (enemy.transform.position.x >= getGameWorld().GameWidth)
            return Vector3.right;

        return Vector3.zero;
    }

    public void onEnemyDestroyed()
    {
        calculateEnemiesSpeed();
        updateCurrentMovingRow();
    }

    private void calculateEnemiesSpeed()
    {
        float ratio = (float)getEnemiesCount() / (float)(getSpawner().EnemiesRows * getSpawner().EnemiesCols);
        enemiesSpeed = enemiesMaxSpeed - (enemiesMaxSpeed - enemiesInitialSpeed) * ratio;
    }

    private int getEnemiesCount()
    {
        int result = 0;
        for (int i = 0; i < getSpawner().EnemiesRows; i++)
            for (int j = 0; j < getSpawner().EnemiesCols; j++)
            {
                if (getEnemies()[i, j] != null)
                    result++;
            }

        return result;
    }

    private GameObject[,] getEnemies()
    {
        return getSpawner().Enemies;
    }

    private EnemiesSpawnBehavior getSpawner()
    {
        return gameObject.GetComponent<EnemiesSpawnBehavior>();
    }

    private GameWorldBehavior getGameWorld()
    {
        return gameObject.GetComponent<GameWorldBehavior>();
    }
}
