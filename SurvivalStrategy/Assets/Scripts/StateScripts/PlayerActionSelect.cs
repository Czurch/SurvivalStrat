using System.Collections;
using UnityEngine;

internal class PlayerActionSelect : State
{
    public PlayerActionSelect(TurnSystem ts) : base(ts)
    {
    }

    public override IEnumerator Start()
    {
        turnSystem.Titletext.text = "Action Select Phase";
        yield return new WaitForSeconds(5.0f);
        //start a timer for 30 seconds
        //wait for all players to be ready or time to run out

        turnSystem.SetState(new ConflictResolution(turnSystem));
    }

    
}