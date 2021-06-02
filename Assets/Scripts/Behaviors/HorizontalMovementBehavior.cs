using UnityEngine;

public class HorizontalMovementBehavior: MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float screenWidth = 0;

    void Update()
    {
        moveObject();
    }

    private void moveObject()
    {
        Vector3 newPosition = Vector3.right * GetMovingInput() * speed * Time.deltaTime;
        newPosition += transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth, screenWidth);
        transform.position = newPosition;
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
