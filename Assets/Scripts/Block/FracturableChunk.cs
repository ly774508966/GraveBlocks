using System.Collections.Generic;

public class FracturableChunk : IFracturableChunk
{
    private List<IBlock> blocks;

    private FracturableChunk(List<IBlock> blocks)
    {
        this.blocks = blocks;
    }

    #region interface
    public List<IBlock> GetBlocks()
    {
        return blocks;
    }
    #endregion

    #region creator
    // eventually change T[,] to the blocks container type
    public static List<IFracturableChunk> UpdateFracturables<T>(T[,] blocks, int minWidth = 2, int minHeight = 2) where T : IFracturable, IBlock
    {
        // first reset all blocks's fracturables to false
        for (var x = 0; x < blocks.GetLength(0); x++)
        {
            // change if the grid isn't supposed to be squared
            for (var y = 0; y < blocks.GetLength(1); y++)
            {
                if (blocks[x, y] != null) // are empty blocks supposed to be null?
                    blocks[x, y].IsFracturable = false;
            }
        }

        // get all rectangles O(N^2*M^2), ordered by size
        var rectangles = FindAllRectangles(blocks, minWidth, minHeight);
        var results = new List<IFracturableChunk>();
        // loop all rectangles O(N*M*R), create FracturableChunk and set all blocks to fracturable
        // if a rectangle has any fracturable one then it's looping with a better rectangle, so we can ignore it
        foreach (var rectangle in rectangles)
        {
            var failure = false;
            for (var x = rectangle.x; x <= rectangle.endX && !failure; x++)
            {
                for (var y = rectangle.y; y <= rectangle.endY; y++)
                {
                    if (blocks[x, y].IsFracturable)
                    {
                        failure = true;
                        break;
                    }
                }
            }
            if (!failure)
            {
                var thisBlocks = new List<IBlock>(rectangle.size);
                for (var x = rectangle.x; x <= rectangle.endX; x++)
                {
                    for (var y = rectangle.y; y <= rectangle.endY; y++)
                    {
                        blocks[x, y].IsFracturable = true;
                        thisBlocks.Add(blocks[x, y]);
                    }
                }
                results.Add(new FracturableChunk(thisBlocks));
            }
        }
        return results;
    }

    private struct SimpleRectangle
    {
        public readonly int x;
        public readonly int y;
        public readonly int endX;
        public readonly int endY;
        public readonly int size;

        public SimpleRectangle(int sx, int sy, int ex, int ey)
        {
            x = sx;
            y = sy;
            endX = ex;
            endY = ey;
            size = (endX - x + 1) * (endY - y + 1);
        }
    }

    private static List<SimpleRectangle> FindAllRectangles<T>(T[,] blocks, int minWidth = 2, int minHeight = 2) where T : IFracturable, IBlock
    {
        var results = new List<SimpleRectangle>();
        var width = blocks.GetLength(0);
        var height = blocks.GetLength(1);
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var blockStart = blocks[x, y];
                if (blockStart == null) continue;
                var maxX = width;
                var maxY = height;
                for (var endX = x; endX < maxX; endX++)
                {
                    for (var endY = y; endY < maxY; endY++)
                    {
                        var block = blocks[endX, endY];
                        if (block == null)
                        {
                            maxY = endY;
                            break;
                        }
                        else
                        {
                            if (endX - x + 1 >= minWidth && endY - y + 1 >= minHeight)
                                results.Add(new SimpleRectangle(x, y, endX, endY));
                        }
                    }
                }
            }
        }
        results.Sort((a, b) => a.size > b.size ? -1 : 1);
        return results;
    }
    #endregion
}
