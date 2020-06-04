using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditor.iOS;
using System;
using System.Linq;

public class Scorekeeper : MonoBehaviour
{
    public int starting_num;
    public int player_count;
    public TextMeshProUGUI[] playertext;
    public TextMeshProUGUI Titletext;
    public Player[] player;
    public DraftSpawner spawner;

    public static Scorekeeper SK;
    void Awake()
    {
        if (SK != null)
            GameObject.Destroy(SK);
        else
            SK = this;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        player_count = 4;
        for (int i = 0; i < player_count * 3; i++)
        {
            spawner.SpawnSurvivor();
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerText(Player p)
    {
        int ix = Array.IndexOf(player, p);
        if (p.isDead)
        {
            playertext[ix].color = Color.red;
            playertext[ix].fontStyle = FontStyles.Strikethrough;
            playertext[ix].text = "Player " + (ix + 1) + ": " + player[ix].survivors.Count;
        }
        else
        {
            playertext[ix].text = "Player " + (ix + 1) + ": " + player[ix].survivors.Count;
        }
    }

     public void CheckGameStatus() 
    {
        //we only check game status when a player's colony is wiped out
        player_count--;
        if (player_count == 1)
        {
            int winner = 0;
            for(int ix = 0; ix < 4; ix++)
            {
                if (player[ix].survivors.Count != 0)
                {
                    winner = ix+1;
                }
            }
            Titletext.color = Color.green;
            Titletext.text = "Player " + winner + " Wins!";
        }
    }

    public void AdvanceToNextRound()
    {
        for (int ix = 0; ix < 4; ix++)
        {
            player[ix].Hydrate();
            player[ix].Feed();
        }
    }
}
