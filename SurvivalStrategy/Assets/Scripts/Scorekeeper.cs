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
        for (int i = 0; i < player_count; i++)
        {
            spawner.SpawnSurvivors();
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddSurvivor(int player_ix)
    {
        Survivor s = ScriptableObject.CreateInstance<Survivor>();
        s.SetStats("Gary", 3, 1);
        player[player_ix].AddSurvivor(s);
        Debug.Log("Player " + (player_ix+1) + " +1  = " + player[player_ix].survivors.Count);
        //SK.UpdatePlayerText(player[player_ix]);
    }

    public void RemoveSurvivor(int player_ix)
    {
        int count = player[player_ix].survivors.Count;
        player[player_ix].RemoveSurvivor(player[player_ix].survivors.ElementAt(count - 1));
        Debug.Log("Player " + (player_ix+1) + " -1  = " + player[player_ix].survivors.Count);
        if (player[player_ix].isDead)
        {
            CheckGameStatus();
        }
        //SK.UpdatePlayerText(player[player_ix]);
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
