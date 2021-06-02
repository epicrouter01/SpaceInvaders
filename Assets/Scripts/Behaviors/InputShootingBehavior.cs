using UnityEngine;
using System;

public class InputShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletSpeed = 0;
    [SerializeField] private float screenHeight = 0;

    private Action<GameObject> onCollisionCallback;
    private GameObject bullet;

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
        if (bullet.transform.position.y >= screenHeight)
        {
            destroyBullet();
        }
    }

    public void registerCollisionCallback(Action<GameObject> cb)
    {
        onCollisionCallback = cb;
    }

    public void destroyBullet()
    {
        if (bullet == null) return;
        Destroy(bullet.gameObject);
        bullet = null;
    }

    private void onPlayerBulletCollision(GameObject gameObject)
    {
        if (onCollisionCallback != null)
            onCollisionCallback(gameObject);
    }

}
