using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemiesSpawnBehavior : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private int enemiesRows = 0;
    [SerializeField] private int enemiesCols = 0;

    private GameObject[,] enemies;

    public GameObject[,] Enemies { get => enemies; }
    public int EnemiesRows { get => enemiesRows; set => enemiesRows = value; }
    public int EnemiesCols { get => enemiesCols; set => enemiesCols = value; }

    public void spawnEnemies()
    {
        GameObject enemy;
        Vector3 position;

        enemies = new GameObject[enemiesRows, enemiesCols];
        for (int i = 0; i < enemiesRows; i++)
        {
            for (int j = 0; j < enemiesCols; j++)
            {
                enemy = Instantiate(enemyPrefab);
                enemy.GetComponent<EnemyBehavior>().Color = generateEnemyColor();
                enemy.GetComponent<EnemyBehavior>().Row = i;
                enemy.GetComponent<EnemyBehavior>().Col = j;

                position = new Vector3(-getGameWorld().GameWidth + j * 2.27f, getGameWorld().GameHeight - 1 - i * 2.3f, 0);
                enemy.transform.position = position;
                Enemies[i, j] = enemy;
            }
        }
    }

    public EnemyBehavior.Colors generateEnemyColor()
    {
        Array colors = Enum.GetValues(typeof(EnemyBehavior.Colors));
        return (EnemyBehavior.Colors)colors.GetValue(UnityEngine.Random.Range(0, colors.Length));
    }

    public void removeEnemy(int row, int col)
    {
        Destroy(enemies[row, col]);
        enemies[row, col] = null;
    }

    public void removeEnemies()
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

    public bool isEmpty()
    {
        for (int i = 0; i < enemiesRows; i++)
            for (int j = 0; j < enemiesCols; j++)
                if (enemies[i, j] != null)
                    return false;

        return true;
    }

    private GameWorldBehavior getGameWorld()
    {
        return gameObject.GetComponent<GameWorldBehavior>();
    }
}
