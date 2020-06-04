using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    public new string name;
    public string description;
    public Sprite artwork;

    public int max_health;
    public int health;
    public int combat_score;
    public bool isThirsty;
    public bool isHungry;
    public bool isDead;
    public Player controlling_player;
    public Bunk bunk;
    public Item[] items;

    public void SetStats(string n, int h, int cs)
    {
        name = n;
        health = h;
        combat_score = cs;
        isThirsty = true;
        isHungry = true;
        isDead = false;
        items = new Item[2];
    }

    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
        combat_score = 1;
        isThirsty = true;
        isHungry = true;
        isDead = false;
        items = new Item[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        health--;
        if (health <= 0)
        {
            isDead = true;
            Debug.Log(name + " has died... Rest in Peace");
        }
    }

    public void takeAction()
    {
        isHungry = true;
    }

    public void BindToPlayer(Player p)
    {
        controlling_player = p;
    }
}
