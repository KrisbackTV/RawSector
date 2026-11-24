using UnityEngine;

public enum GameMachineState
{
    Menu,
    SaveLoadMenu,
    Gameplay,
    Inventory,
    EscMenu,
    Dialogue,
    Dead
}

public abstract class GameState : ScriptableObject
{
    public GameMachineState _state;

    public virtual void EnterState()
    {
    }

    public virtual void UpdateState()
    {
    }

    public virtual void ExitState()
    {
    }
}
