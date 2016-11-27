using UnityEngine;
using System.Collections;
using System;

public class GridController : MonoBehaviour
{

    public static readonly string BASE_NAME = "Grid";

    public Transform hexPrefab;

    public int gridWidth = 11;
    public int gridHeight = 11;

    private float hexWidth = 0.9f;
    private float hexHeight = 1.0f;
    private float gap = 1f;

    public Vector3 startPosition;


    // Use this for initialization
    void Start()
    {
        AddGapOffset();
        CalculateStartPosition();
        CreateGrid();
    }

    private void AddGapOffset()
    {
        hexWidth += (hexWidth * gap);
        hexHeight += hexHeight * gap;
    }

    private void CalculateStartPosition()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
        {
            offset = gridWidth / 2;
        }

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPosition = new Vector3(x, 0, z);
    }

    private void CreateGrid()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPosition = new Vector2(x, y);

                hex.position = CalculateWorldPosition(gridPosition);
                hex.parent = this.transform;
                hex.name = HexTile.DEFAULT_NAME + x + HexTile.DEFAULT_DELIMITER + y;

                HexTile tile = hex.GetComponent<HexTile>();
                tile.x = x;
                tile.y = y;

                //DetermineTerrain(ref tile);

            }
        }
    }

    private void DetermineTerrain(ref HexTile tile)
    {
        //TODO implement logic to generate terrain
        //(random implementation for now to test clicking
        //features.)
        int rand = (int)Math.Round(UnityEngine.Random.value);

        if (rand == 1)
        {
            tile.isTraversable = false;
            tile.transform.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
        }
    }

    public static void ResetAllTiles()
    {
        GameObject grid = GameObject.Find(GridController.BASE_NAME);
        if (grid != null)
        {
            MeshRenderer[] renderers = grid.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material.color = Color.white;
            }
        }
    }

    private Vector3 CalculateWorldPosition(Vector2 gridPosition)
    {
        float offset = 0.0f;
        if (gridPosition.y % 2 != 0)
        {
            offset = hexWidth / 2;
        }

        float x = startPosition.x + gridPosition.x * hexWidth + offset;
        float z = startPosition.z + gridPosition.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
