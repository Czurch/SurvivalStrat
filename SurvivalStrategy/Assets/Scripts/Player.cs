using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    int player_index;
    private Compound compound;
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
        survivors.Add(s);
        Gamemanager.GM.UpdatePlayerText(this);

        if (compound.bunks_available.Count != 0)
        {
          Bunk bunk = compound.bunks_available.Pop();
          compound.bunks_occupied.Push(bunk);
          //survivors.Add(holder.survivor);
          //holder.controlling_player = this;
        }
        Gamemanager.GM.DS.PickCharacter();
    }

    public void RemoveSurvivor(Survivor s)
    {
        s.bunk.Unbind();
        survivors.Remove(s);
        if (survivors.Count <= 0)
        {
            isDead = true;
            Gamemanager.GM.CheckGameStatus();
        }
        Gamemanager.GM.UpdatePlayerText(this);
    }

    //Uses water resources each round to keep characters alive
    public void Hydrate()
    {
        for (int ix = 0; ix < survivors.Count; ix++)
        {
            Survivor[] list = survivors.ToArray<Survivor>();
            if (list[ix].isHungry)
            {
                if (water > 0)
                {
                    water--;
                    Debug.Log("feeding " + list[ix].name);
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
                    Debug.Log("feeding " + list[ix].name);
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
