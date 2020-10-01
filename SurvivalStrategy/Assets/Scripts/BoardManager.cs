using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int boardSizeX;
    public int boardSizeY;
    public float tileOffset;
    public GameObject tile;
    private GameObject[,] board;
    List<TileSpace> tileSpaces;

    // Start is called before the first frame update
    void Start()
    {
        tileSpaces = new List<TileSpace>();
        //we initialize our board to the correct size and set a Tile in each place

        board = new GameObject[boardSizeX, boardSizeY];

        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeX; y++)
            {
                board[x, y] = Instantiate(tile, new Vector3(x * tileOffset, 0, y * tileOffset), Quaternion.identity);
                board[x, y].transform.SetParent(gameObject.transform);
                board[x, y].name = x+","+y;
                tileSpaces.Add(board[x, y].GetComponent<TileSpace>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeX; y++)
            {
                board[x, y].transform.position = new Vector3(gameObject.transform.position.x + x * tileOffset, 0, gameObject.transform.position.z + y * tileOffset);
            }
        }
    }

    public List<TileSpace> uncontestedTiles()
    {
        List<TileSpace> lootTiles = new List<TileSpace>();
        foreach (TileSpace tile in tileSpaces)
        {
            if (tile.hasOnlyOnePlayer())
            {
                lootTiles.Add(tile);
            }
        }
        return lootTiles;
    }

    public List<TileSpace> tilesWithConflict()
    {
        List<TileSpace> conflictTiles = new List<TileSpace>();
        foreach (TileSpace tile in tileSpaces)
        {
            if (tile.isConflict)
            {
                conflictTiles.Add(tile);
            }
        }
        return conflictTiles;

    }
}
