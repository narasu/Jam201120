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
        timer = 0.8f;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) owner.GotoState(enemy.GetRandomState());
    }

    public override void Exit()
    {

    }
}

public class SweepState : EnemyState
{
    public override void Enter()
    {
        enemy.animator.SetBool("isClapping", true);
    }

    public override void Update()
    {
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            owner.GotoState(EnemyStateType.Idle);
        }
    }

    public override void Exit()
    {
        enemy.animator.SetBool("isClapping", false);
    }
}

public class SlamState : EnemyState
{
    bool shockwavesSpawned;
    
    public override void Enter()
    {
        enemy.animator.SetBool("isSlamming", true);
        shockwavesSpawned = false;
    }

    public override void Update()
    {
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.472f && !shockwavesSpawned)
        {
            enemy.SpawnShockwaves();
            shockwavesSpawned = true;
        }

        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            owner.GotoState(EnemyStateType.Idle);
        }
    }

    public override void Exit()
    {
        enemy.animator.SetBool("isSlamming", false);
    }
}

public class RainState : EnemyState
{
    int numProjectiles = 20;
    float projectileFrequency = 0.1f;

    float startDelay;
    bool started;
    public override void Enter()
    {
        startDelay = 0.5f;
        enemy.animator.SetBool("isCasting", true);
        enemy.finishedFiring = false;
        started = false;
    }

    public override void Update()
    {
        if (startDelay > 0) startDelay -= Time.deltaTime;
        else if (!started)
        {
            started = true;
            IEnumerator c = enemy.SpawnProjectiles(numProjectiles, projectileFrequency);
            enemy.StartCoroutine(c);
        }
        
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            owner.GotoState(EnemyStateType.Idle);
        }
    }

    public override void Exit()
    {
        enemy.animator.SetBool("isCasting", false);
    }
}