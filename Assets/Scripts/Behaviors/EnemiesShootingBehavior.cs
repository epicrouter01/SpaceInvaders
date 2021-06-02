using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemiesShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletsSpeed = 0;
    private readonly float minInterval = 0.1f;
    private readonly float maxInterval = 0.8f;
    private readonly int maxShots = 5;

    private Action<GameObject, GameObject> onCollisionCallback;
    private float shootingInterval = 0;
    private List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        generateNextInterval();
    }

    // Update is called once per frame
    void Update()
    {
        shootingInterval -= Time.deltaTime;
        if (shootingInterval <= 0)
        {
            makeShot();
            generateNextInterval();
        }
        moveBullets();
        destroyWastedBullets();
    }

    private void moveBullets()
    {
        foreach (GameObject i in bullets)
        {
            i.transform.position += Vector3.down * bulletsSpeed * Time.deltaTime;
        }
    }

    private void destroyWastedBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].transform.position.y <= -getWorld().GameHeight)
            {
                destroyBullet(bullets[i]);
                i--;
            }
        }
    }

    public void destroyBullet(GameObject bullet)
    {
        int index;
        index = bullets.IndexOf(bullet);
        if (index != -1)
            bullets.RemoveAt(index);

        Destroy(bullet);
    }

    private void generateNextInterval()
    {
        shootingInterval = UnityEngine.Random.Range(minInterval, maxInterval);
    }

    private void makeShot()
    {
        if (bullets.Count >= maxShots)
            return;

        shootWith(findEligibleShooter());
    }

    private void shootWith(GameObject shooter)
    {
        if (shooter == null) return;
        bullets.Add(createBullet(shooter));
    }

    private GameObject createBullet(GameObject shooter)
    {
        GameObject result;
        result = Instantiate(bulletPrefab);
        result.AddComponent<CollisionDetectionBehavior>().registerCollisionCallback(onBulletCollision);
        result.name = "Bullet";
        result.transform.position = shooter.transform.position;
        return result;
    }

    public void registerCollisionCallback(Action<GameObject, GameObject> action)
    {
        onCollisionCallback = action;
    }

    private void onBulletCollision(GameObject target, GameObject bullet)
    {
        if (onCollisionCallback != null)
            onCollisionCallback(target, bullet);
    }

    private GameObject findEligibleShooter()
    {
        List<GameObject> list = getLowestEnemies();
        return list[new System.Random().Next(list.Count)];
    }

    private List<GameObject> getLowestEnemies()
    {
        List<GameObject> result = new List<GameObject>();
        GameObject enemy;

        for (int i = 0; i < getSpawner().EnemiesCols; i++)
        {
            enemy = getLowestEnemy(i);
            if (enemy != null)
                result.Add(enemy);
        }

        return result;
    }

    private GameObject getLowestEnemy(int col)
    {
        for(int i = getSpawner().EnemiesRows - 1; i >= 0; i--)
        {
            if (getSpawner().Enemies[i, col] != null)
                return getSpawner().Enemies[i, col];
        }

        return null;
    }

    private EnemiesSpawnBehavior getSpawner() {
        return gameObject.GetComponent<EnemiesSpawnBehavior>();
    }

    private GameWorldBehavior getWorld()
    {
        return gameObject.GetComponent<GameWorldBehavior>();
    }
}
