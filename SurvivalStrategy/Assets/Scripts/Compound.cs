using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compound : MonoBehaviour
{
    public GameObject bunkPrefab;
    public GameObject[] slot_object;
    private Stack<GameObject> bunk_slot;
    public Stack<Bunk> bunks_available;
    public Stack<Bunk> bunks_occupied;
    public Player controlling_player;

    // Start is called before the first frame update
    void Start()
    {
        controlling_player = gameObject.GetComponentInParent<Player>();
        bunks_available = new Stack<Bunk>();
        bunks_occupied = new Stack<Bunk>();
        bunk_slot = new Stack<GameObject>();
        bunk_slot.Push(slot_object[0]);
        bunk_slot.Push(slot_object[1]);
        bunk_slot.Push(slot_object[2]);
        bunk_slot.Push(slot_object[3]);
        bunk_slot.Push(slot_object[4]);
        bunk_slot.Push(slot_object[5]);
        bunk_slot.Push(slot_object[6]);
        bunk_slot.Push(slot_object[7]);

        AddBunk();
        AddBunk();
        AddBunk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Instantiates a new bunk in the compound
    public void AddBunk()
    {
        GameObject bunkObj = Instantiate(bunkPrefab, bunk_slot.Pop().transform.position, gameObject.transform.rotation);
        bunkObj.transform.SetParent(gameObject.transform);
        bunks_available.Push(bunkObj.GetComponent<Bunk>());
    }

    public void BindSurivorToBunk(Survivor s)
    {
        Bunk bunk = bunks_available.Pop();
        s.gameObject.GetComponent<SurvivorHolder>().bunk_occupied = bunk;
        bunks_occupied.Push(bunk);
    }
}
