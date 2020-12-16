using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PathFindingAStar : MonoBehaviour
{
    public WorldGrid worldGrid;

    [SerializeField] private const int DiagonalDistance = 14;
    [SerializeField] private const int PlaneDistance = 10;


    public List<Cell> FindPath(Vector3 startingPos, Vector3 endPosition)
    {
        Cell startingCell = worldGrid.GetCellFromWorldPoint(startingPos);
        Cell endCell = worldGrid.GetCellFromWorldPoint(endPosition);

        
        
        if (!endCell.IsWalkable || startingCell == endCell) return null;


        HeapStructure<Cell> openCells = new HeapStructure<Cell>(worldGrid.ToUnidimensionSize);
        HashSet<Cell> closedCells = new HashSet<Cell>();

        openCells.Add(startingCell);

        while (openCells.Count > 0)
        {
            Cell currentCell = openCells.RemoveFirst();


            closedCells.Add(currentCell);

            if (currentCell == endCell)
            {
                return GetPath(startingCell, endCell);
            }

            foreach (Cell surroundingCell in worldGrid.GetSurroundingCells(currentCell))
            {
                if (surroundingCell.IsWalkable && !closedCells.Contains(surroundingCell))
                {
                    int newMovementCostToNeighbour =
                        currentCell.GCost + GetDistance(currentCell, surroundingCell);
                    if (newMovementCostToNeighbour < surroundingCell.GCost || !openCells.Contains(surroundingCell))
                    {
                        surroundingCell.GCost = newMovementCostToNeighbour;
                        surroundingCell.HCost = GetDistance(surroundingCell, endCell);
                        surroundingCell.Parent = currentCell;

                        if (!openCells.Contains(surroundingCell))
                        {
                            openCells.Add(surroundingCell);
                        }
                    }
                }
            }
        }

        return null;
    }


    List<Cell> GetPath(Cell startingCell, Cell endCell)
    {
        List<Cell> path = new List<Cell>();
        Cell currentNode = endCell;

        while (currentNode != startingCell)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();

        return path;
    }


    public int GetDistance(Cell origin, Cell target)
    {
        int distanceX = (int) Mathf.Abs(target.GridCoordinates.x - origin.GridCoordinates.x);
        int distanceZ = (int) Mathf.Abs(target.GridCoordinates.y - origin.GridCoordinates.y);


        // returns distance between both cells 
        if (distanceX == distanceZ) return DiagonalDistance * distanceX;
        else if (distanceX > distanceZ) return DiagonalDistance * distanceZ + PlaneDistance * distanceX;
        else return DiagonalDistance * distanceX + PlaneDistance * distanceZ;
    }
}