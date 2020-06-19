using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    int player_index;
    public Compound compound;
    public bool isDead;

    public int water;
    public int food;
    public int scrap;

    public List<Survivor> survivors;
    private 
    // Start is called before the first frame update
    void Start()
    {
        compound = gameObject.GetComponentInChildren<Compound>();
        survivors = new List<Survivor>();

        water = 15;
        food = 15;
        scrap = 0;
    }

    //Assigns a Survivor to a bunk and adds him to the player's control
    public void AddSurvivor(Survivor s)
    {
        if (compound.bunks_available.Count != 0)
        {
            survivors.Add(s);
            Gamemanager.GM.stateMachine.UpdatePlayerText(this);
            compound.BindSurivorToBunk(s);
            s.BindToPlayer(this);
        }
        Gamemanager.GM.stateMachine.DS.PickCharacter();
    }

    public void RemoveSurvivor(Survivor s)
    {
        s.gameObject.GetComponent<SurvivorHolder>().bunk_occupied.Unbind();
        survivors.Remove(s);
        Destroy(s.gameObject);
        if (survivors.Count <= 0)
        {
            isDead = true;
            Gamemanager.GM.stateMachine.CheckGameStatus();
        }
        Gamemanager.GM.stateMachine.UpdatePlayerText(this);
    }

    //Uses water resources each round to keep characters alive
    public void Hydrate()
    {
        for (int ix = 0; ix < survivors.Count; ix++)
        {
            Survivor[] list = survivors.ToArray<Survivor>();
            if (list[ix].isThirsty)
            {
                if (water > 0)
                {
                    water--;
                    //Debug.Log("hydrating " + list[ix].name);
                }
                else
                {
                    list[ix].takeDamage();
                    if(list[ix].isDead)
                    {
                        RemoveSurvivor(list[ix]);
                    }
                    
                }
            }
        }
    }

    //Uses food resources to keep hungry characters alive
    public void Feed()
    {
        for (int ix = 0; ix < survivors.Count; ix++)
        {
            Survivor[] list = survivors.ToArray<Survivor>();
            if (list[ix].isHungry)
            {
                if (food > 0)
                {
                    food--;
                    //Debug.Log("feeding " + list[ix].name);
                }
                else
                {
                    list[ix].takeDamage();
                    if (list[ix].isDead)
                    {
                        RemoveSurvivor(list[ix]);
                    }
                }
            }
        }

    }

    public void BuyBunk()
    {
        if(scrap >= 10)
        { 
            scrap -= 10;
            compound.AddBunk();
        }
    }
}
