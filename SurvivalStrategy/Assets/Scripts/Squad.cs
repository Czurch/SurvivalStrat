using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    List<Survivor> survivors;
    private int combat_score;

    public Transform[] survivor_slot;
    // Start is called before the first frame update
    void Start()
    {
        survivors = new List<Survivor>();
        combat_score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this is called when a play is dropped onto the Squad container
    public Vector3 addSurvivor(Survivor s)
    {
        if (survivors.Count > 3)
        {
            //squad is full
        }
        else
        {
            survivors.Add(s);
            combat_score += s.combat_score;
            return survivor_slot[survivors.Count - 1];
        }
    }

    //this is called when the player is removed from the Squad container
    public void removeSurvivor(Survivor s)
    {
        if (survivors.Contains(s))
        {
            survivors.Remove(s);
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
}
