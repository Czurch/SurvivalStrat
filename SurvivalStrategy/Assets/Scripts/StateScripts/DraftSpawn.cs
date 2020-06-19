using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraftSpawn : State
{
    public DraftSpawn(TurnSystem turnSystem) : base(turnSystem)
    { 
    
    }

    public override IEnumerator Start()
    {
        for (int i = 0; i < turnSystem.player_count * turnSystem.starting_num_survivors; i++)
        {
            GameObject temp = turnSystem.DS.SpawnSurvivor();
            turnSystem.free_agents.Push(temp);
        }
        yield return new WaitForSeconds(1f);
        turnSystem.SetState(new CharacterDraft(turnSystem));
    }
}



