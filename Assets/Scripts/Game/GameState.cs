﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateType { Play, Win, Dead }

public abstract class GameState
{
    protected GameFSM owner;
    protected GameManager gm;

    public void Initialize(GameFSM _owner)
    {
        owner = _owner;
        gm = _owner.owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class PlayState : GameState
{
    public override void Enter()
    {
        
    }

    public override void Update()
    {
        gm.timer -= Time.deltaTime;

        if (gm.timer <= 0) owner.GotoState(GameStateType.Win);
    }

    public override void Exit()
    {
        
    }
}

public class WinState : GameState
{
    public override void Enter()
    {
        gm.door.SetActive(true);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}

public class DeadState : GameState
{
    public override void Enter()
    {
        //ded
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }
}