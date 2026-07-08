using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

[CreateAssetMenu(fileName = "GameStateMachine", menuName = "Architecture/StateMachine")]
public class GameStateMachine : ScriptableObject
{
    private Stack<GameState> _stateStack = new Stack<GameState>();

    public GameState CurrentState => _stateStack.Count > 0 ? _stateStack.Peek() : null;

    public void Initialize()
    {
        _stateStack.Clear();
    }

    public void PushState(GameState newState)
    {
        if (newState == null || newState == CurrentState) return;

        CurrentState?.ExitState();

        _stateStack.Push(newState);
        newState.EnterState();

        Debug.Log($"current state: {CurrentState.name}, stack count: {_stateStack.Count}");
    }

    public void PopState()
    {
        if (_stateStack.Count <= 1)
        {
            Debug.Log("cant pop, last state!");
        }

        _stateStack.Pop().ExitState();

        CurrentState?.EnterState();
        Debug.Log($"Poped state:{CurrentState?.name}");
    }

    public void ChangeState(GameState newState)
    {
        while (_stateStack.Count > 0)
        {
            _stateStack.Pop().ExitState();
        }
        PushState(newState);
    }

    public void ExecuteTick() => CurrentState?.UpdateState();
}
