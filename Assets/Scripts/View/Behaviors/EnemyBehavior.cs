using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites = null;

    private int row;
    private int col;
    public enum Colors {
        Blue = 0,
        Red = 1,
        Green = 2,
        Yellow = 3
    }

    private Colors color;
    private bool flicked = false;

    public Colors Color { 
        get => color; 
        set {
            color = value;
            updateSprite();
        } 
    }

    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void updateSprite()
    {
        int frame = (int)color + (flicked ? 4 : 0);
        GetComponent<SpriteRenderer>().sprite = sprites[frame];
    }

    public void flick()
    {
        flicked = !flicked;
        updateSprite();
    }
}
