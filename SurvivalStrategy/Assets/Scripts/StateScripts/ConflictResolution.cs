using System.Collections;
using UnityEngine;

internal class ConflictResolution : State
{
    public ConflictResolution(TurnSystem ts) : base(ts)
    {
    }

    public override IEnumerator Start()
    {
        turnSystem.Titletext.text = "Resolution Phase";
        //if any opposing survivors are on the same tile
        //   let those survivors fight
        //give loot to th winner and everyone who was on their own tile
        yield return new WaitForSeconds(1.0f);

        turnSystem.SetState(new EndRound(turnSystem));
    }

    public override IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.0f);
        turnSystem.SetState(new GameOver(turnSystem));
    }
}