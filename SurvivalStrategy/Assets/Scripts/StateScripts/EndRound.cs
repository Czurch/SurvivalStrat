using System.Collections;
using UnityEditorInternal;
using UnityEngine;

internal class EndRound : State
{
    public EndRound(TurnSystem ts) : base(ts)
    {
    }

    public override IEnumerator Start()
    {
        turnSystem.Titletext.text = "End Round";
        yield return new WaitForSeconds(1.0f);
        //if all but one player is dead;
        //else we calculate the end of round stuff
        // items and buildings are crafted

        // all survivors consume one water, if not they lose 1 hp
        FeedAndHydrate();

        //then we go back to loading the loot
        if (turnSystem.gameOver)
        {
            turnSystem.SetState(new GameOver(turnSystem));
        }
        else
        {
            turnSystem.SetState(new LoadLoot(turnSystem));
        }
    }

    public void FeedAndHydrate()
    {
        for (int ix = 0; ix < 4; ix++)
        {
            turnSystem.players[ix].Hydrate();
            turnSystem.players[ix].Feed();
        }
    }
}