using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Survivor", menuName = "Survivor")]
public class Survivor : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public int health;
    public int combat_score;
    public bool isThirsty;
    public bool isHungry;
    public bool isDead;
    public Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        combat_score = 1;
        isThirsty = false;
        isHungry = false;
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
        }
    }
}
