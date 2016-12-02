public class Block : IBlock, IFracturable // monobehaviour?
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsFracturable { get; set; }
}