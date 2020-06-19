using System.Collections;
using UnityEngine;

internal class LoadLoot : State
{
    public LoadLoot(TurnSystem ts) : base(ts)
    {
    }

    public override IEnumerator Start()
    {
        turnSystem.Titletext.text = "Load Loot Phase";
        yield return new WaitForSeconds(1.0f);
        //Load the loot onto the gameboard

        turnSystem.SetState(new PlayerActionSelect(turnSystem));
    }
}