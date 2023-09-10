using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStateMachine : MonoBehaviour
{
    public HeroState currentState { get; private set;}//read only
    // Start is called before the first frame update
    public void Initialize(HeroState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(HeroState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
