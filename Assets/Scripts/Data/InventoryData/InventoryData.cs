using Newtonsoft.Json;
using System.Collections.Generic;
using System;

[Serializable]
public sealed class InventoryData
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public float CellSizeX;
    public float CellSizeY;

    public List<InventoryItem> Items { get; private set; }

    [JsonConstructor]
    public InventoryData(
       int width,
       int height,
       float cellSizeX,
       float cellSizeY,
       List<InventoryItem> items)
    {
        Width = width;
        Height = height;
        CellSizeX = cellSizeX;
        CellSizeY = cellSizeY;
        Items = items ?? new List<InventoryItem>();
    }

    public InventoryData(int width, int height, float cellSizeX,
       float cellSizeY, IEnumerable<InventoryItem> items)
    {
        Width = width;
        Height = height;
        CellSizeX = cellSizeX;
        CellSizeY = cellSizeY;
        Items = items != null
           ? new List<InventoryItem>(items)
           : new List<InventoryItem>();
    }

    public InventoryData()
    {
        Items = new List<InventoryItem>();
    }
}