using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpace : MonoBehaviour
{
    public bool isOccupied;
    public List<Player> player_occupants;
    // Start is called before the first frame update
    void Start()
    {
        isOccupied = false;
        player_occupants = new List<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bind(Player p)
    {
        isOccupied = true;
        player_occupants.Add(p);
    }

    public void Unbind(Player p)
    {
        player_occupants.Remove(p);
        if (player_occupants.Count == 0)
        {
            isOccupied = false;
        }
    }
}
