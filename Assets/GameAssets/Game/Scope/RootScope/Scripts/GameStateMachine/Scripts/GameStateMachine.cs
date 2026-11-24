using System.Collections.Generic;

public class GameStateMachine
{
    private Stack<GameState> _stateStack = new Stack<GameState>();
    public GameState CurrentState => _stateStack.Count > 0 ? _stateStack.Peek() : null;

    public void Start()
    {
        _stateStack.Clear();
    }

    public void PushState(GameMachineState stateType)
    {
        //if (_assetsLibrarySO.GetGameStateForType(stateType) == CurrentState) return;

        //GameState newState = _assetsLibrarySO.GetGameStateForType(stateType);

        //CurrentState?.ExitState();

        //_stateStack.Push(newState);
        //newState.EnterState();

        //GameLog.LogInfo($"current state: {CurrentState.name}, stack count: {_stateStack.Count}");
    }

    public void PopState()
    {
        if (_stateStack.Count <= 1)
        {
            GameLog.LogWarning("cant pop, last state!");
        }

        _stateStack.Pop().ExitState();

        CurrentState?.EnterState();
        GameLog.LogInfo($"Poped state:{CurrentState?.name}");
    }

    public void ChangeState(GameMachineState stateType)
    {
        while (_stateStack.Count > 0)
        {
            _stateStack.Pop().ExitState();
        }

        PushState(stateType);
    }

    public void ExecuteTick() => CurrentState?.UpdateState();
}
