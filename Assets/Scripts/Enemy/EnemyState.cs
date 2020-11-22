using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType { Idle, Sweep, Slam, Rain }

public abstract class EnemyState
{
    protected EnemyFSM owner;
    protected Enemy enemy;

    public void Initialize(EnemyFSM _owner)
    {
        owner = _owner;
        enemy = _owner.owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class IdleState : EnemyState
{
    float timer;
    public override void Enter()
    {
        timer = 2.0f;
        
    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) owner.GotoState(EnemyStateType.Rain);
    }

    public override void Exit()
    {

    }
}

public class SweepState : EnemyState
{
    public override void Enter()
    {
        owner.GotoState(EnemyStateType.Idle);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}

public class SlamState : EnemyState
{
    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }
}

public class RainState : EnemyState
{
    int numProjectiles = 5;
    float projectileFrequency = 0.4f;

    public override void Enter()
    {
        enemy.finishedFiring = false;
        IEnumerator c = enemy.SpawnProjectiles(numProjectiles, projectileFrequency);
        enemy.StartCoroutine(c);
    }

    public override void Update()
    {
        if (enemy.finishedFiring) owner.GotoState(EnemyStateType.Idle);
    }

    public override void Exit()
    {

    }
}