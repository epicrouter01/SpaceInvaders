using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float bulletSpeed = 0;
    [SerializeField] private GameObject playerBulletPrefab = null;

    public float Speed { get => speed; set => speed = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public GameObject PlayerBulletPrefab { get => playerBulletPrefab;}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
