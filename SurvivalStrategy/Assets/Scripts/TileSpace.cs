using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpace : MonoBehaviour
{
    public bool isOccupied;
    public bool isConflict;
    public List<Player> player_occupants;
    public List<SurvivorHolder> survivor_occupants;
    public GameObject[] slot_objects;
    private Stack<GameObject> slots;

    // Start is called before the first frame update
    void Start()
    {
        isOccupied = false;
        isConflict = false;
        player_occupants = new List<Player>();
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
        if (isOccupied)
        { 
            isConflict = true;
        }

        //Check if the player binding already has a survivor on the tile
        if(player_occupants.Contains(sh.survivor.controlling_player))
        {
          //we should put those survivors into a squad
          //add that squad to the tile
        }
        else{
          isOccupied = true;
          player_occupants.Add(sh.survivor.controlling_player);
          survivor_occupants.Add(sh);
          sh.gameObject.transform.position = slots.Pop().transform.position;
        }
    }

    public void Unbind(SurvivorHolder sh)
    {
        //player_occupants.Remove(p);
        survivor_occupants.Remove(sh);
        if (survivor_occupants.Count == 0)
        {
            isOccupied = false;
        }
    }
}
