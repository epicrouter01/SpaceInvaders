using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDestroyerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab = null;
    private EnemiesDestroyStrategy enemiesDestroyStrategy = new EnemiesDestroyStrategy();

    public int destroyEnemy(EnemyBehavior enemy)
    {
        List <BoardTile> list = enemiesDestroyStrategy.getDestroyedEnemies(createTiles(), enemy.Row, enemy.Col);
        foreach (BoardTile tile in list)
        {
            setExplosion(getSpawner().Enemies[tile.X, tile.Y]);
            getSpawner().removeEnemy(tile.X, tile.Y);
        }

        return list.Count;
    }

    private void setExplosion(GameObject enemy)
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = enemy.transform.position;
        explosion.GetComponent<ExplosionBehavior>().setColor(enemy.GetComponent<EnemyBehavior>().Color);
    }

    private BoardTile[,] createTiles()
    {
        BoardTile[,] result = new BoardTile[getSpawner().EnemiesRows, getSpawner().EnemiesCols];

        for (int i = 0; i < getSpawner().EnemiesRows; i++)
            for (int j = 0; j < getSpawner().EnemiesCols; j++)
            {
                if (getSpawner().Enemies[i, j] == null) continue;

                result[i, j] = new BoardTile(i, j, (int)getSpawner().Enemies[i, j].GetComponent<EnemyBehavior>().Color);
            }

        return result;
    }

    private EnemiesSpawnBehavior getSpawner()
    {
        return GetComponent<EnemiesSpawnBehavior>();
    }
}
