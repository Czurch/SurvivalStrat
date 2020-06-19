using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Gamemanager : MonoBehaviour
{
    public GameObject stateObject;
    public TurnSystem stateMachine;
    public bool randomDraft;
    //public int player_count;

    public static Gamemanager GM;
    //Ensure this script is a Singleton
    void Awake()
    {
        if (GM != null)
            GameObject.Destroy(GM);
        else
            GM = this;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = stateObject.GetComponent<TurnSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
