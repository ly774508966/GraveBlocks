using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class FracturableChunkTests
{
    [TestFixtureSetUp]
    public void Init()
    {
    }

    [TestFixtureTearDown]
    public void Dispose()
    {
    }

    [Test]
    public void TestFracturableCreator()
    {
        // 6x6 world
        var worldString = @"111101
111101
111100
011000
001111
001110".Split('\n');
        var blocks = CreateBlocksFromString(worldString, 6);
        var fracturableChunks = FracturableChunk.UpdateFracturables(blocks);
        Assert.That(fracturableChunks.Count, Is.EqualTo(2));
        Assert.That(fracturableChunks[0].GetBlocks().Count, Is.EqualTo(12));
        Assert.That(fracturableChunks[1].GetBlocks().Count, Is.EqualTo(6));
    }

    private Block[,] CreateBlocksFromString(string[] worldString, int size)
    {
        var blocks = new Block[size, size];
        for (var y = 0; y < size; y++)
        {
            var worldLine = worldString[y];
            for (var x = 0; x < size; x++)
            {
                if (worldLine[x] == '1')
                {
                    var block = new Block
                    {
                        X = x,
                        Y = y
                    };
                    blocks[x, y] = block; // change this if block becomes a monobehaviour
                }
            }
        }
        return blocks;
    }
}