using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State state;

    public void SetState(State s)
    {
        state = s;
        StartCoroutine(state.Start());
    }

    public State GetState()
    {
        return state;
    }

}
