using System.Collections.Generic;

public class PlayerFSM
{
    public Player owner { get; private set; }

    private Dictionary<PlayerStateType, PlayerState> states;

    public PlayerStateType CurrentStateType { get; private set; }
    private PlayerState currentState;
    private PlayerState previousState;

    public void Initialize(Player _owner)
    {
        owner = _owner;
        states = new Dictionary<PlayerStateType, PlayerState>();
    }

    public void AddState(PlayerStateType _newType, PlayerState _newState)
    {
        states.Add(_newType, _newState);
        states[_newType].Initialize(this);
    }

    public void UpdateState()
    {
        currentState?.Update();
    }

    public void GotoState(PlayerStateType _key)
    {
        if (!states.ContainsKey(_key))
        {
            return;
        }

        currentState?.Exit();

        previousState = currentState;
        CurrentStateType = _key;
        currentState = states[CurrentStateType];

        currentState.Enter();
    }

    public PlayerState GetState(PlayerStateType _type)
    {
        if (!states.ContainsKey(_type))
        {
            return null;
        }
        return states[_type];
    }
}