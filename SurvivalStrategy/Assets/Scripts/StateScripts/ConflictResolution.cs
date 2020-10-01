using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

internal class ConflictResolution : State
{
    public ConflictResolution(TurnSystem ts) : base(ts)
    {
    }

    public override IEnumerator Start()
    {
        turnSystem.Titletext.text = "Resolution Phase";
        List<TileSpace> uncontestedTiles = turnSystem.board_manager.uncontestedTiles();
        List<TileSpace> conflictTiles = turnSystem.board_manager.tilesWithConflict();
        if (conflictTiles.Count > 0)
        {
            foreach (TileSpace tile in conflictTiles)
            {
                Player p = tile.ResolveConflict();
                Debug.Log("Player " + p.name);
            }
        }
        //give loot to th winner and everyone who was on their own tile



        //TODO: 1. Create the abstract items class
        //      2. Create a small list of available items
        //      3. Make rudimentary loot spawn system for TileSpace
        //      4. Create Character item spaces
        //      5. Allow Winner to drag items onto Character item spaces
        //      6. Allow Players to drag items from Character item spaces to inventory




        yield return new WaitForSeconds(1.0f);

        turnSystem.SetState(new EndRound(turnSystem));
    }

    public override IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.0f);
        turnSystem.SetState(new GameOver(turnSystem));
    }
}