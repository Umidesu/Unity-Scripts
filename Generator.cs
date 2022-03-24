using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject player;

    private GameObject lastTile;
    private GameObject firstTile;

    public float distanceBetweenTiles = 3.0f;
    public float randomValue = 0.8f;

    public float chunkSize = 5;

    private float tileCounter = 0;

    private Vector3 previousTilePosition;
    private Vector3 direction,
        mainDirection = new Vector3(0, 0, 1),
        otherDirection = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        // Gets first tile as reference tile
        previousTilePosition = tileToSpawn.transform.position;

        // Creates first tiles amonth of chunk size
        for (int i = 0; i < chunkSize; i++)
        {
            CreateTile(mainDirection);
        }
        UpdateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (CalcDistance(player.transform.position, firstTile.transform.position) > distanceBetweenTiles * 2)
        {
            DeleteTile();
            if (Random.value < randomValue)
            {
                direction = mainDirection;
            }
            else
            {
                Vector3 temp = direction;
                direction = otherDirection;
                mainDirection = direction;
                otherDirection = temp;
            }
            CreateTile(direction);
            UpdateTiles();
        }
    }

    // Create next tile
    void CreateTile(Vector3 Direction)
    {
        tileCounter++;

        // Sets spawn posion of the tile
        Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * Direction;

        // Create tile to position
        GameObject Tile = Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
        Tile.name = tileToSpawn.name + tileCounter;

        previousTilePosition = spawnPos;
    }

    // Deletes the tile at out of the chunk
    void DeleteTile()
    {
        Destroy(GameObject.Find(tileToSpawn.name + (tileCounter - chunkSize)));
    }

    // Updates the first and last tiles
    void UpdateTiles()
    {
        lastTile = GameObject.Find(tileToSpawn.name + tileCounter);
        firstTile = GameObject.Find(tileToSpawn.name + (tileCounter - (chunkSize - 1)));
    }

    // Calculates distance between to tiles
    public float CalcDistance(Vector3 Tile1, Vector3 Tile2)
    {
        float Distance;
        Distance = Mathf.Sqrt((Tile1.x - Tile2.x) * (Tile1.x - Tile2.x)
                            + (Tile1.y - Tile2.y) * (Tile1.y - Tile2.y)
                            + (Tile1.z - Tile2.z) * (Tile1.z - Tile2.z)
        );
        return Distance;
    }
}
