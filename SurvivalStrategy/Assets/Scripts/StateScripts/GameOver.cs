using System.Collections;
using UnityEngine;

internal class GameOver : State
{
    public GameOver(TurnSystem ts) : base(ts)
    {
    }

    public override IEnumerator Start()
    {
        int winner = 0;
        for (int ix = 0; ix < 4; ix++)
        {
            if (turnSystem.players[ix].survivors.Count != 0)
            {
                winner = ix + 1;
            }
        }

        turnSystem.Titletext.color = Color.green;
        turnSystem.Titletext.text = "Player " + winner + " Wins!";
        yield break;
        //Game is over, show endgame screen
    }
}