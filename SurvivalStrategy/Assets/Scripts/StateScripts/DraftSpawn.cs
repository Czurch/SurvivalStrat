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
        for (int i = 0; i < player_count * starting_num_survivors; i++)
        {
            DS.SpawnSurvivor();
        }
        yield break;
    }
}
