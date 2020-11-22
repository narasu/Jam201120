using System.Collections.Generic;

public class EnemyFSM
{
    public Enemy owner { get; private set; }

    private Dictionary<EnemyStateType, EnemyState> states;

    public EnemyStateType CurrentStateType { get; private set; }
    private EnemyState currentState;
    private EnemyState previousState;

    public void Initialize(Enemy _owner)
    {
        owner = _owner;
        states = new Dictionary<EnemyStateType, EnemyState>();
    }

    public void AddState(EnemyStateType _newType, EnemyState _newState)
    {
        states.Add(_newType, _newState);
        states[_newType].Initialize(this);
    }

    public void UpdateState()
    {
        currentState?.Update();
    }

    public void GotoState(EnemyStateType _key)
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

    public EnemyState GetState(EnemyStateType _type)
    {
        if (!states.ContainsKey(_type))
        {
            return null;
        }
        return states[_type];
    }
}