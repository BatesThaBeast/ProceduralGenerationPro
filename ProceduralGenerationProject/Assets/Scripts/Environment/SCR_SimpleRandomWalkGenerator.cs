using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SCR_SimpleRandomWalkGenerator : SCR_AbstractDungeonGenerator
{
    [SerializeField]
    private SCR_SimpleRandomWalkSO randomWalkParameters;
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RundRandomWalk(randomWalkParameters);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        SCR_WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RundRandomWalk(SCR_SimpleRandomWalkSO parameters)
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = SCR_ProcGen.SimpeRandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path);
            if (parameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
