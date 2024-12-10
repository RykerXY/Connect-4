using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject tilePrefab; // Assign your Tile prefab here
    public int rows = 6;
    public int columns = 7;

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
{
    for (int row = 0; row < rows; row++) // Loop through rows
    {
        for (int col = 0; col < columns; col++) // Loop through columns
        {
            // Instantiate a new tile
            GameObject tile = Instantiate(tilePrefab, transform);

            // Set position using localPosition
            tile.transform.localPosition = new Vector3(col * 1.1f, -row * 1.1f, 0); // Adjust spacing (1.1f adds padding)

            // Optional: Initialize tile script
            TileScript tileScript = tile.GetComponent<TileScript>();
            if (tileScript != null)
            {
                tileScript.Initialize(row, col);
            }
        }
    }
}
}
