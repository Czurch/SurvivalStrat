using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TileSpace : MonoBehaviour
{
    public string title;
    public bool isConflict;
    public Squad[] squads;
    public Item[] itemList;

    // Start is called before the first frame update
    void Start()
    {
        isConflict = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bind(SurvivorHolder sh)
    {
        int px = sh.survivor.controlling_player.player_index;
        if (squads[px].controlling_player == null)
        {
            squads[px].controlling_player = sh.survivor.controlling_player;
        }
            
        sh.slot_occupied = squads[px].addSurvivor(sh.survivor);

        isConflict = isOccupied(px);
    }

    public void Unbind(SurvivorHolder sh)
    {
        int px = sh.survivor.controlling_player.player_index;
        squads[px].removeSurvivor(sh.survivor, sh.slot_occupied);
        isConflict = checkConflict();
    }

    public Player ResolveConflict()
    {
        int max = 0;
        Player p = new Player();
        foreach (Squad s in squads)
        {
            if (s.getCombatScore() > max)
                p = s.controlling_player;
        }

        return p;
    }

    // Spawns items based on the type of tile
    public void SpawnLoot()
    {
    
    }

    public bool isOccupied(int p_ix)
    {
        bool occupied = false;
        for (int x = 0; x < 4; x++)
        {
            if (x != p_ix)
            {
                if (!squads[x].isEmpty())
                {
                    occupied = true;
                    break;
                }
            }
        }
        return occupied;
    }

    public bool checkConflict()
    {
        int occupied = 0;
        for (int x = 0; x < 4; x++)
        {
            if (!(squads[x].isEmpty()))
            {
                occupied++;
            }
        }
        return (occupied > 1);
    }

    public bool hasOnlyOnePlayer()
    {
        int occupied = 0;
        for (int x = 0; x < 4; x++)
        {
            if (!(squads[x].isEmpty()))
            {
                occupied++;
            }
        }
        return (occupied == 1);
    }
}
