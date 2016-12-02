
using System.Collections.Generic;

// an IFracturableChunk can have multiple blocks inside (and every block can have a different parent chunk)
public interface IFracturableChunk
{
    List<IBlock> GetBlocks();
}
