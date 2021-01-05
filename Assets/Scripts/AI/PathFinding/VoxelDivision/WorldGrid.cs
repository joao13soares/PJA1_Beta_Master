using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{




    [SerializeField] private LayerMask unWalkableLayer;

    [SerializeField] private float worldSizeX;
    [SerializeField] private float worldSizeZ;

    [SerializeField] private float cellRadius;
    private float cellDiameter;

    private int numberCellsGridX;
    private int numberCellsGridZ;

    [SerializeField] private Cell[,] worldCells;

    public int ToUnidimensionSize => numberCellsGridZ * numberCellsGridX;









    private void Awake()
    {
        cellDiameter = cellRadius * 2;
        numberCellsGridX = Mathf.RoundToInt(worldSizeX / cellDiameter);
        numberCellsGridZ = Mathf.RoundToInt(worldSizeZ / cellDiameter);

        // Debug.Log(numberCellsGridX + " " + numberCellsGridZ);

        worldCells = CreateCells();









    }





    private void UpdateGrid()
    {
        worldCells = CreateCells();
    }


    Cell[,] CreateCells()
    {
        Cell[,] newGrid = new Cell[numberCellsGridZ, numberCellsGridX];


        Vector3 upperLeftCorner = transform.position - Vector3.right * worldSizeX / 2 +
                                  Vector3.forward * worldSizeZ / 2;


        for (int z = 0; z < numberCellsGridZ; z++)
        {
            for (int x = 0; x < numberCellsGridX; x++)
            {

                Vector3 cellWorldPosition = upperLeftCorner +
                    x * (Vector3.right * cellDiameter) + new Vector3(cellRadius, 0f, 0f) +
                    z * (Vector3.back * cellDiameter) - new Vector3(0f, 0f, cellRadius);



                bool walkable = !((Physics.CheckSphere(cellWorldPosition, cellRadius, unWalkableLayer)) ||
                                  !Physics.CheckSphere(cellWorldPosition, cellRadius));

                newGrid[z, x] = new Cell(walkable, cellWorldPosition, new Vector2(x, z));

            }

        }

        return newGrid;


    }

    public Cell GetCellFromWorldPoint(Vector3 positionToConvert)
    {

        // Gets vector relative to grid center to know which cell corresponds to in case grid moves away from center of world
        Vector3 relativePositionToGrid = positionToConvert - transform.position;

        // Percentages to get indexes in X and Z
        float percentageX = (relativePositionToGrid.x + worldSizeX / 2) / worldSizeX;
        float percentageZ = 1 - (relativePositionToGrid.z + worldSizeZ / 2) / worldSizeZ;


        // Get nearest possible when outside of the grid
        percentageX = Mathf.Clamp01(percentageX);
        percentageZ = Mathf.Clamp01(percentageZ);



        // Gets indexes for the array
        int indexX = Mathf.FloorToInt(Mathf.Clamp((numberCellsGridX) * percentageX, 0, numberCellsGridX - 1));
        int indexZ = Mathf.FloorToInt(Mathf.Clamp((numberCellsGridZ) * percentageZ, 0, numberCellsGridZ - 1));



        return worldCells[indexZ, indexX];
    }

    public List<Cell> GetSurroundingCells(Cell currentCell)
    {


        List<Cell> surroundingCells = new List<Cell>();

        for (int z = -1; z < 2; z++)
        {
            for (int x = -1; x < 2; x++)
            {
                bool isCellReceived = x == 0 && z == 0;
                if (isCellReceived) continue;


                int indexX = (int) currentCell.GridCoordinates.x + x;
                int indexZ = (int) currentCell.GridCoordinates.y + z;

                bool isXValid = indexX >= 0 && indexX < numberCellsGridX;
                bool isZValid = indexZ >= 0 && indexZ < numberCellsGridZ;

                //
                // Debug.Log("ARRAYSIZE: Z: " +numberCellsGridZ + "ARRAYSIZE: X " + numberCellsGridX);
                // Debug.Log("INDEX Z: " + indexZ + "INDEX X: " + indexX );

                if (isXValid && isZValid) surroundingCells.Add(worldCells[indexZ, indexX]);

            }
        }

        return surroundingCells;

    }



    // // DEBUG DRAWING, FUCK FPS IN THIS FASE
    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireCube(transform.position, new Vector3(worldSizeX, 1f, worldSizeZ));
    //
    //
    //     if (worldCells == null) return;
    //
    //     foreach (Cell c in worldCells)
    //     {
    //         if (!c.IsWalkable)
    //         {
    //             Gizmos.color = Color.red;
    //             Gizmos.DrawCube(c.Position, new Vector3(cellDiameter, 1f, cellDiameter));
    //
    //
    //         }
    //        
    //         
    //
    //
    //
    //
    //
    //
    //     }
    //
    //
    // }
}

