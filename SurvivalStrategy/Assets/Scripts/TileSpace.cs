using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpace : MonoBehaviour
{
    public bool isConflict;
    public GameObject[] slot_objects;
    public Squad[] squads;
    private Stack<GameObject> slots;

    // Start is called before the first frame update
    void Start()
    {
        isConflict = false;
        squads = new Squad[4];
        for (int i = 0; i < 4; i++)
        {
            slots.Push(slot_objects[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bind(SurvivorHolder sh)
    {
        int px = sh.survivor.controlling_player.player_index;
        squads[px].addSurvivor(sh.survivor);
        sh.slot_occupied = slots.Pop();

        isConflict = isOccupied(px);
    }

    public void Unbind(SurvivorHolder sh)
    {
        int px = sh.survivor.controlling_player.player_index;

        squads[px].removeSurvivor(sh.survivor, sh.slot_occupied);
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
}
