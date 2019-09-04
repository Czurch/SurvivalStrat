using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject compound;
    public bool isDead;

    public int water;
    public int food;
    public int scrap;

    public List<Survivor> survivors;
    public Stack<Bunk> bunks_available;
    public Stack<Bunk> bunks_occupied;

    // Start is called before the first frame update
    void Start()
    {
        survivors = new List<Survivor>();
        bunks_available = new Stack<Bunk>();
        bunks_occupied = new Stack<Bunk>();
        water = 15;
        food = 15;
        scrap = 0;
    }

    //Assigns a Survivor to a bunk and adds him to the player's control
    public void AddSurvivor(SurvivorHolder holder)
    {
        if (bunks_available.Count != 0)
        {
          Bunk bunk = bunks_available.Pop();
          bunk.Bind(holder);
          bunks_occupied.Push(bunk);
          survivors.Add(holder.survivor);
          holder.controlling_player = this;
        }
    }

    //Instantiates a new bunk in the compound
    public void AddBunk()
    {
        Bunk bunk = new Bunk();
        bunk.CreateNewBunk(gameObject.transform.position, gameObject.transform.rotation);
        bunks_available.Push(bunk);
    }

    //Uses water resources each round to keep characters alive
    public void Hydrate()
    {
        foreach (Survivor s in survivors)
        {
            if (s.isThirsty)
            {
                if (water != 0)
                {
                    water--;
                }
                else
                {
                    s.takeDamage();
                }
            } 
        }
    }

    //Uses food resources to keep hungry characters alive
    public void Feed()
    {
        foreach (Survivor s in survivors)
        {
            if (s.isHungry)
            {
                if (food != 0)
                {
                    food--;
                }
                else
                {
                    s.takeDamage();
                }
            }
        }
    }
}
