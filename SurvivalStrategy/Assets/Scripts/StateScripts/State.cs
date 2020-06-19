using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected TurnSystem turnSystem;

    public State(TurnSystem ts)
    {
        turnSystem = ts;
    }
    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator EndGame()
    {
        yield break;
    }
}