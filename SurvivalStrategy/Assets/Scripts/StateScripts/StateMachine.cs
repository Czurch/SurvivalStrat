using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State state;
    protected IEnumerator routine;
    public void SetState(State s)
    {
        state = s;
        routine = state.Start();
        StartCoroutine(routine);
    }

    public void KillState()
    {
        StopCoroutine(routine);
    }

    public State GetState()
    {
        return state;
    }

}
