using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    Animator stateMachine;
    //set our Hash values for performance
    int charSelectHash   = Animator.StringToHash("CharactersDrafted");
    int lootHash         = Animator.StringToHash("LootLoaded");
    int playersReadyHash = Animator.StringToHash("AllPlayersReady");
    int conflictHash     = Animator.StringToHash("ConflictResolved");
    int rOHash           = Animator.StringToHash("RoundOver");
    int GameOverHash     = Animator.StringToHash("GameOver");

    private int numberOfPlayers;
    public Player[] players;
    public BoardManager board_manager;

    public int startingNumOfCharacters;

    // Start is called before the first frame update
    void Start()
    {
        numberOfPlayers = 2;
        stateMachine = GetComponent<Animator>();
        players = new Player[numberOfPlayers];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //stateMachine.SetBool("");
        }
        //stateMachine.SetBool("");   
    }

    void CharacterSelect()
    {
        for (int i = 1; i < startingNumOfCharacters; i++)
        {
            //iterate through the players and allow them to pick a survivor
            //allow this player to pick a survivor
            //order should be 1-2-3-4, 4-3-2-1, 2-4-1-3, 3-1-4-2
            //survivors should randomize each round
        }

        stateMachine.SetBool(charSelectHash, true);
    }

    void LoadLoot()
    {
        stateMachine.SetBool(charSelectHash, false);
        //Load the loot onto the gameboard
        stateMachine.SetBool(lootHash, true);
    }

    void PlayerActionSelect()
    {
        stateMachine.SetBool(lootHash, false);
        //start a timer for 30 seconds
        //wait for all players to be ready or time to run out
        stateMachine.SetBool(playersReadyHash, true);
    }

    void ConflictResolution()
    {
        stateMachine.SetBool(playersReadyHash, false);
        //if any opposing survivors are on the same tile
        //   let those survivors fight
        //give loot to th winner and everyone who was on their own tile


        stateMachine.SetBool(conflictHash, true);
    }

    void EndofRound()
    {
        stateMachine.SetBool(conflictHash, false);
        //if all but one player is dead
        stateMachine.SetBool(GameOverHash, true);
        //else we calculate the end of round stuff
        // items and buildings are crafted
        // all survivors consume one water
        // if not they lose 1 hp

        //then we go back to loading the loot
        stateMachine.SetBool(rOHash, true);
    }

}
