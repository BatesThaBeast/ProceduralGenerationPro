using System;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CorridorFirstDungeonGenerator : SCR_SimpleRandomWalkGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent;
    [SerializeField]
    public SCR_SimpleRandomWalkSO roomGenerationParameters;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        SCR_WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions)
    {
        var currentPosition = startPosition;
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = SCR_ProcGen.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            floorPositions.UnionWith(corridor);
        }
    }
}
