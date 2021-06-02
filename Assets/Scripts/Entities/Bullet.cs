using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    Action<GameObject> onCollisionCallback;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onCollisionCallback != null)
        {
            onCollisionCallback(collision.gameObject);
        }
    }

    public void registerCollisionCallback(Action<GameObject> cb)
    {
        onCollisionCallback = cb;
    }
}
