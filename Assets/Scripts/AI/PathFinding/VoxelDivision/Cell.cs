using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : IHeapItem<Cell>
{
    public bool IsWalkable;
    public Vector3 Position;
    public Vector2 GridCoordinates;

    public int GCost;
    public int HCost;

    public Cell Parent;

    private int heapIndex;

    public int HeapIndex
    {
        get => heapIndex;
        set => heapIndex = value;
    }


    public Cell(bool walkable, Vector3 worldPosition, Vector2 arrayCoordinates)
    {
        IsWalkable = walkable;
        Position = worldPosition;
        GridCoordinates = arrayCoordinates;
    }

    public int FCost => GCost + HCost;


    public int CompareTo(Cell cellToCompare)
    {
        int comparison = FCost.CompareTo(cellToCompare.FCost);

        if (comparison == 0) comparison = HCost.CompareTo(cellToCompare.HCost);

        return -comparison;

    }
}