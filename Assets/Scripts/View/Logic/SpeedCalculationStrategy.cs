
public class SpeedCalculationStrategy
{
    public float calculateSpeed(int enemiesCurrentCount, int enemiesInitialCount, float initialSpeed, float maxSpeed)
    {
        float ratio = (float)enemiesCurrentCount / (float)enemiesInitialCount;
        return maxSpeed - (maxSpeed - initialSpeed) * ratio;
    }
}
