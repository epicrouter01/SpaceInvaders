public class BoardTile
{
    private int x;
    private int y;
    private int value;
    private bool marked = false;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public bool Marked { get => marked; set => marked = value; }
    public int Value { get => value; set => this.value = value; }

    public BoardTile(int x, int y, int value)
    {
        this.x = x;
        this.y = y;
        this.value = value;
    }
}
