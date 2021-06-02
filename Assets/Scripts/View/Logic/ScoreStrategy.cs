public class ScoreStrategy
{
    public int getScore(int destroyedCount)
    {
        return destroyedCount * 10 * calculateFibonacci(destroyedCount + 1);
    }

    private int calculateFibonacci(int n)
    {
        if ((n == 0) || (n == 1))
            return n;

        return calculateFibonacci(n - 1) + calculateFibonacci(n - 2);
    }
}
