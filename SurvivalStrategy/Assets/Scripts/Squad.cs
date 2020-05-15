using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    public int max_squad_members;
    private Player controlling_player;
    List<Survivor> survivors;
    public GameObject[] slot_object;
    private Stack<GameObject> survivor_slot;
    private int combat_score;

    
    // Start is called before the first frame update
    void Start()
    {
        max_squad_members = 3;
        survivors = new List<Survivor>();
        survivor_slot = new Stack<GameObject>();
        survivor_slot.Push(slot_object[0]);
        survivor_slot.Push(slot_object[1]);
        survivor_slot.Push(slot_object[2]);
        survivor_slot.Push(slot_object[3]);
        combat_score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this is called when a play is dropped onto the Squad container
    public GameObject addSurvivor(Survivor s)
    {
        if (survivors.Count > (slot_object.Length - 1))
        {
            //squad is full
            Debug.Log("Squad is Full");
            return null;
        }
        else
        {
            survivors.Add(s);
            combat_score += s.combat_score;
            Debug.Log("Squad Count:" + survivors.Count);
            return survivor_slot.Pop();
        }
    }

    //this is called when the player is removed from the Squad container
    public void removeSurvivor(Survivor s, GameObject slot)
    {
        if (survivors.Contains(s))
        {
            survivors.Remove(s);
            survivor_slot.Push(slot);
            Debug.Log("Squad Count:" + survivors.Count);
            combat_score -= s.combat_score;
        }
        else {
            Debug.Log("Error: Selected Survivor was not found in Squad's List");
        }
    }

    public int getCombatScore()
    {
        return combat_score;
    }

    public void setPlayer(Player p)
    {
        controlling_player = p;
    }

    public Player getPlayer()
    {
        return controlling_player;
    }
}
