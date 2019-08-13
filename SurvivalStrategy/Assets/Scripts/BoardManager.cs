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

    // Start is called before the first frame update
    void Start()
    {
        //we initialize our board to the correct size and set a Tile in each place

        board = new GameObject[boardSizeX, boardSizeY];

        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeX; y++)
            {
                board[x, y] = Instantiate(tile, new Vector3(x * tileOffset, 0, y * tileOffset), Quaternion.identity);
                board[x, y].transform.SetParent(gameObject.transform);
                board[x, y].name = x+","+y;
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
                board[x, y].transform.position = new Vector3(x * tileOffset, 0, y * tileOffset);
            }
        }
    }
}
