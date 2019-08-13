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
    public Item[] items;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        items = new Item[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
