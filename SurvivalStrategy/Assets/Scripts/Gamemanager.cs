using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Gamemanager : MonoBehaviour
{
    Animator stateMachine;
    private IEnumerator activeRoutine;
    private IEnumerator draftRoutine;
    private IEnumerator lootRoutine;
    private IEnumerator actionRoutine;
    private IEnumerator resolutionRoutine;
    private IEnumerator roundRoutine;
    //set our Hash values for performance
    int charSelectHash   = Animator.StringToHash("CharactersDrafted");
    int lootHash         = Animator.StringToHash("LootLoaded");
    int playersReadyHash = Animator.StringToHash("AllPlayersReady");
    int conflictHash     = Animator.StringToHash("ConflictResolved");
    int rOHash           = Animator.StringToHash("RoundOver");
    int GameOverHash     = Animator.StringToHash("GameOver");

    public int player_count;
    public int starting_num_survivors;
    public Player[] players;
    public BoardManager board_manager;
    public DraftSpawner DS;

    public TextMeshProUGUI[] playertext;
    public TextMeshProUGUI Titletext;

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
        stateMachine = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckActiveState();
    }

    public void SurvivorSpawn()
    {
        for (int i = 0; i < player_count * starting_num_survivors; i++)
        {
            DS.SpawnSurvivor();
        }
    }

    public IEnumerator CharacterDraft()
    {
        Titletext.text = "Character Select Phase";

        while(DS.free_agents != 0)
        {
            yield return null;
            //iterate through the players and allow them to pick a survivor
            //allow this player to pick a survivor
            //order should be 1-2-3-4, 4-3-2-1, 2-4-1-3, 3-1-4-2
            //survivors should randomize each round
        }

        stateMachine.SetBool(charSelectHash, true);
        yield return new WaitForSeconds(5.0f);
    }

    public IEnumerator LoadLoot()
    {
        stateMachine.SetBool(charSelectHash, false);
        Titletext.text = "Load Loot Phase";
        yield return new WaitForSeconds(3.0f);
        //Load the loot onto the gameboard
        stateMachine.SetBool(lootHash, true);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator PlayerActionSelect()
    {
        stateMachine.SetBool(lootHash, false);
        Titletext.text = "Action Select Phase";
        //start a timer for 30 seconds
        //wait for all players to be ready or time to run out
        stateMachine.SetBool(playersReadyHash, true);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator ConflictResolution()
    {
        stateMachine.SetBool(playersReadyHash, false);
        Titletext.text = "Resolution Phase";
        //if any opposing survivors are on the same tile
        //   let those survivors fight
        //give loot to th winner and everyone who was on their own tile


        stateMachine.SetBool(conflictHash, true);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator EndRound()
    {
        stateMachine.SetBool(conflictHash, false);
        Titletext.text = "End of Round";
        //if all but one player is dead
        stateMachine.SetBool(GameOverHash, true);
        //else we calculate the end of round stuff
        // items and buildings are crafted
        // all survivors consume one water
        // if not they lose 1 hp

        //then we go back to loading the loot
        stateMachine.SetBool(rOHash, true);
        yield return new WaitForSeconds(0.1f);
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
        if (player_count == 1)
        {
            int winner = 0;
            for (int ix = 0; ix < 4; ix++)
            {
                if (players[ix].survivors.Count != 0)
                {
                    winner = ix + 1;
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
            players[ix].Hydrate();
            players[ix].Feed();
        }
    }


    //CheckAndSetRoutine is used to check active state and start the correlated coroutine
    public void CheckAndSetRoutine(String state, IEnumerator i)
    {
        if (stateMachine.GetCurrentAnimatorStateInfo(0).IsName(state))
        {
            if (activeRoutine != i)
            {
                if (activeRoutine != null)
                    StopCoroutine(activeRoutine);
                activeRoutine = i;
                StartCoroutine(activeRoutine);
            }
        }
    }

    public void CheckActiveState()
    {
        CheckAndSetRoutine("CharacterDraft", CharacterDraft());
        CheckAndSetRoutine("LoadLoot", LoadLoot());
        CheckAndSetRoutine("PlayerActionSelect", PlayerActionSelect());
        CheckAndSetRoutine("ConflictResolution", ConflictResolution());
        CheckAndSetRoutine("EndRound", EndRound());
    }
}
