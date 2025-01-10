using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class GridPointHandler : MonoBehaviour
{
    public VisualEffect gridPointVFX; // The Visual Effect Graph component that will be used to display the grid point
    public Transform playerTransform; // The player transform to center the grid around
    public float cellSize = 1f; // Size of each grid cell
    public float updateThreshold = 0.5f; // Distance threshold to update the grid
    public int gridSize = 11; // Size of the grid (must be an odd number)

    private Vector3 lastPlayerPosition;
    private HashSet<Vector3Int> populatedPoints = new HashSet<Vector3Int>(); // Tracks populated grid points
    private Queue<Vector3Int> pointsToPopulate = new Queue<Vector3Int>(); // Queue of grid points to process

    [Serializable]
    public struct GridPoint
    {
        public Vector3Int position;
        public bool isPopulated;

        public GridPoint(Vector3Int position, bool isPopulated)
        {
            this.position = position;
            this.isPopulated = isPopulated;
        }
    }

    void Start()
    {
        lastPlayerPosition = playerTransform.position;
        GenerateGridPoints();
    }

    void Update()
    {
        // Check if the player has moved more than the threshold
        if (Vector3.Distance(playerTransform.position, lastPlayerPosition) > updateThreshold)
        {
            lastPlayerPosition = playerTransform.position;
            GenerateGridPoints();
        }

        // Process one grid point per frame if available
        if (pointsToPopulate.Count > 0)
        {
            Vector3Int gridPointPosition = pointsToPopulate.Dequeue();
            populatedPoints.Add(gridPointPosition);

            // Debug visualization
            Debug.DrawRay((Vector3)gridPointPosition * cellSize, Vector3.up * 0.25f, Color.green, 5f);

            // Set the position in the VFX graph
            gridPointVFX.SetVector3("SpawnGridPoint", (Vector3)gridPointPosition * cellSize);

            // Spawn the grid point
            gridPointVFX.SendEvent("SpawnGridPoint");
        }
    }

    void GenerateGridPoints()
    {
        // Temporary set for new grid points
        HashSet<Vector3Int> newGridPoints = new HashSet<Vector3Int>();

        // Generate a grid around the player with an adjustable size
        Vector3 centerPosition = playerTransform.position;
        Vector3Int roundedCenter = Vector3Int.RoundToInt(centerPosition / cellSize);
        int halfGridSize = gridSize / 2;

        for (int x = -halfGridSize; x <= halfGridSize; x++)
        {
            for (int y = -halfGridSize; y <= halfGridSize; y++)
            {
                Vector3Int gridPointPosition = new Vector3Int(
                    roundedCenter.x + x,
                    roundedCenter.y + y,
                    0
                );

                newGridPoints.Add(gridPointPosition);

                // If the point is not already populated, enqueue it for processing
                if (!populatedPoints.Contains(gridPointPosition) && !pointsToPopulate.Contains(gridPointPosition))
                {
                    pointsToPopulate.Enqueue(gridPointPosition);
                }
            }
        }

        // Remove points no longer in the grid
        HashSet<Vector3Int> pointsToRemove = new HashSet<Vector3Int>(populatedPoints);
        pointsToRemove.ExceptWith(newGridPoints);

        foreach (var point in pointsToRemove)
        {
            populatedPoints.Remove(point);
            // Debug visualization for removal
            Debug.DrawRay((Vector3)point * cellSize, Vector3.up * 0.25f, Color.red, 5f);
        }
    }
}
