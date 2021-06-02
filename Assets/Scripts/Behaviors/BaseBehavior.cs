public class BaseBehavior
{
    protected float deltaTime;
    public virtual void update(float deltaTime)
    {
        this.deltaTime = deltaTime;
    }
}
