using VContainer.Unity;

public class StateMachineRunner : ITickable
{
    private readonly GameStateMachine _stateMachine;

    public StateMachineRunner(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Tick()
    {
        _stateMachine.ExecuteTick();
    }
}