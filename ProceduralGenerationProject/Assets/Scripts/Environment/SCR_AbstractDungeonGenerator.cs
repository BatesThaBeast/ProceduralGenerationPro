using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SCR_AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected SCR_TileMapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }
    public void ClearDungeon()
    {
        tilemapVisualizer.Clear();
    }
    protected abstract void RunProceduralGeneration();
}
