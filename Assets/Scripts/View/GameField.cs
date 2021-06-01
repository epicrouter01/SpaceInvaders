using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private MainCharacter player = null;
    [SerializeField] private float gameWidth = 0;
    [SerializeField] private float gameHeight = 0;

    private GameObject playerBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleMoveInput();
        handleShootInput();
        movePlayerBullet();
    }

    private void handleMoveInput()
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

    private void movePlayerBullet()
    {
        if (playerBullet == null) return;

        playerBullet.transform.position += Vector3.up * player.BulletSpeed * Time.deltaTime;
        if (playerBullet.transform.position.y >= gameHeight)
        {
            Destroy(playerBullet);
            playerBullet = null;
        }
    }

    private void handleShootInput()
    {
        if (playerBullet != null) return;

        if (Input.GetKey(KeyCode.Space))
        {
            makeShot();
        }
    }

    private void makeShot()
    {
        playerBullet = Instantiate(player.PlayerBulletPrefab);
        playerBullet.name = "PlayerBullet";
        playerBullet.transform.position = player.transform.position;
        //playerBullet.AddComponent<SpriteRenderer>().sprite = sprite;
    }
}
