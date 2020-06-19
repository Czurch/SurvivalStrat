using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class TurnSystem : StateMachine
{
    public int player_count;
    public int starting_num_survivors;
    public Player[] players;
    public Stack<GameObject> free_agents;
    public BoardManager board_manager;
    public DraftSpawner DS;

    public TextMeshProUGUI[] playertext;
    public TextMeshProUGUI Titletext;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        free_agents = new Stack<GameObject>();
        SetState(new DraftSpawn(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerText(Player p)
    {
        int ix = Array.IndexOf(players, p);
        if (p.isDead)
        {
            playertext[ix].color = Color.red;
            playertext[ix].fontStyle = FontStyles.Strikethrough;
            playertext[ix].text = "Player " + (ix + 1) + ": " + players[ix].survivors.Count;
        }
        else
        {
            playertext[ix].text = "Player " + (ix + 1) + ": " + players[ix].survivors.Count;
        }
    }

    public void CheckGameStatus()
    {
        //we only check game status when a player's colony is wiped out
        player_count--;
        gameOver = player_count == 1;
    }
}
