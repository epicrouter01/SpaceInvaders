using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private MainCharacter player = null;
    [SerializeField] private float gameWidth = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        Vector3 newPosition = Vector3.right * GetMovingInput() * player.Speed * Time.deltaTime;
        newPosition += player.transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -gameWidth, gameWidth);
        player.transform.position = newPosition;
    }

    private float GetMovingInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return -1;
        if (Input.GetKey(KeyCode.RightArrow))
            return 1;

        return 0;
    }
}
