using UnityEngine;
using System;

public class InputShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletSpeed = 0;

    private Action<GameObject, GameObject> onCollisionCallback;
    private GameObject bullet;

    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    void Update()
    { 
        handleShootInput();
        moveBullet();
    }

    private void handleShootInput()
    {
        if (bullet != null) return;

        if (Input.GetKey(KeyCode.Space))
        {
            makeShot();
        }
    }

    private void makeShot()
    {
        bullet = Instantiate(bulletPrefab);
        bullet.AddComponent<CollisionDetectionBehavior>().registerCollisionCallback(onPlayerBulletCollision);
        bullet.name = "Bullet";
        bullet.transform.position = transform.position;
    }

    private void moveBullet()
    {
        if (bullet == null) return;

        bullet.transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
        if (bullet.transform.position.y >= getGameWorld().GameHeight)
        {
            destroyBullet();
        }
    }

    public void registerCollisionCallback(Action<GameObject, GameObject> cb)
    {
        onCollisionCallback = cb;
    }

    public void destroyBullet()
    {
        if (bullet == null) return;
        Destroy(bullet.gameObject);
        bullet = null;
    }

    private void onPlayerBulletCollision(GameObject target, GameObject bullet)
    {
        if (onCollisionCallback != null)
            onCollisionCallback(target, bullet);
    }

    private GameWorldBehavior getGameWorld()
    {
        return gameObject.GetComponent<GameWorldBehavior>();
    }
}
