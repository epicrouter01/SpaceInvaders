using UnityEngine;
using System;

public class CollisionDetectionBehavior : MonoBehaviour
{
    private Action<GameObject, GameObject> onCollisionCallback;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onCollisionCallback != null)
        {
            onCollisionCallback(collision.gameObject, gameObject);
        }
    }

    public void registerCollisionCallback(Action<GameObject, GameObject> cb)
    {
        onCollisionCallback = cb;
    }
}
