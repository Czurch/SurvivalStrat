using System.Collections;
using UnityEngine;

internal class CharacterDraft : State
{
    public CharacterDraft(TurnSystem ts) : base(ts)
    {
    }

    public override IEnumerator Start()
    {
        turnSystem.Titletext.text = "Character Select Phase";

        if (Gamemanager.GM.randomDraft)
        {
            //Survivors are drafted at random
            for (int i = 0; i < turnSystem.player_count; i++)
            {
                for (int j = 0; j < turnSystem.starting_num_survivors; j++)
                {
                    GameObject survivor_obj = turnSystem.free_agents.Pop();
                    Survivor survivor = survivor_obj.GetComponent<Survivor>();
                    if (turnSystem.players[i].compound.bunks_available.Count > 0)
                    {
                        turnSystem.players[i].AddSurvivor(survivor);
                    }
                    else 
                    {
                        Debug.Log("player " + i + " ran out of bunks @ " + j);
                        break;
                    }
                }
            }
        }
        else
        {
            while (turnSystem.DS.free_agents != 0)
            {
                yield return null;
                //iterate through the players and allow them to pick a survivor
                //allow this player to pick a survivor
                //order should be 1-2-3-4, 4-3-2-1, 2-4-1-3, 3-1-4-2
                //survivors should randomize each round
            }
        }

        turnSystem.SetState(new LoadLoot(turnSystem));
        yield break;
    }
}