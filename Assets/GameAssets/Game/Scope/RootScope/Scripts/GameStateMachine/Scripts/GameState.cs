using UnityEngine;

public enum GameCoreState
{
    Menu,
    Gameplay,
    Inventory,
    EscMenu,
    Dialogue
}

public abstract class GameState : ScriptableObject
{
    public GameCoreState _state;

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
