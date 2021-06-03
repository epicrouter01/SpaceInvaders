using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites = null;
    [SerializeField] private float delay = 0;

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
            Destroy(gameObject);
    }

    public void setColor(EnemyBehavior.Colors color)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[(int)color];
    }
}
