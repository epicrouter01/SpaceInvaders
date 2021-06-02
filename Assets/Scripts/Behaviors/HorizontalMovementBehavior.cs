using UnityEngine;

public class HorizontalMovementBehavior: BaseBehavior
{
    private float speed;
    private float screenWidth;
    private GameObject target;

    public HorizontalMovementBehavior(GameObject target, float screenWidth, float speed)
    {
        this.target = target;
        this.screenWidth = screenWidth;
        this.speed = speed;
    }

    public override void update(float deltaTime)
    {
        base.update(deltaTime);
        moveObject();
    }

    private void moveObject()
    {
        Vector3 newPosition = Vector3.right * GetMovingInput() * speed * deltaTime;
        newPosition += target.transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth, screenWidth);
        target.transform.position = newPosition;
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
