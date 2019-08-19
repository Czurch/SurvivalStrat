using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isDead;

    public int water;
    public int food;
    public int scrap;

    public List<Survivor> survivors;

    // Start is called before the first frame update
    void Start()
    {
        water = 15;
        food = 15;
        scrap = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddSurvivor(SurvivorDisplay sd)
    {
        survivors.Add(sd.survivor);
        sd.controlling_player = this;
    }

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
