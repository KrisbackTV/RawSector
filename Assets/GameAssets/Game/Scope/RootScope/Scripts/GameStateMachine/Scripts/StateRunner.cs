
public class StateMachineRunner
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