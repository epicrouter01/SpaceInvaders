using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldBehavior : MonoBehaviour
{
    [SerializeField] private float gameWidth = 0;
    [SerializeField] private float gameHeight = 0;

    public float GameWidth { get => gameWidth; set => gameWidth = value; }
    public float GameHeight { get => gameHeight; set => gameHeight = value; }
}
