using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType { Normal, Diving, Rolling }

public abstract class PlayerState
{
    protected PlayerFSM owner;
    protected Player player;

    public void Initialize(PlayerFSM _owner)
    {
        owner = _owner;
        player = _owner.owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class NormalState : PlayerState
{
    CharacterController ctrl;
    public override void Enter()
    {
        ctrl = player.controller;
    }

    public override void Update()
    {
        
        player.grounded = ctrl.isGrounded;
        if (player.grounded && player.velocity.y < 0)
        {
            player.velocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (move != Vector3.zero)
        {
            player.transform.forward = move;
        }
        
        ctrl.Move(move * Time.deltaTime * player.speed);

        if (Input.GetButtonDown("Jump") && player.grounded)
        {
            Debug.Log("jumping");
            player.velocity.y += Mathf.Sqrt(player.jumpHeight * -3.0f * player.gravity);
        }
        player.velocity.y += player.gravity * Time.deltaTime;
        ctrl.Move(player.velocity * Time.deltaTime);

        if (Input.GetButtonDown("Dodge"))
        {
            owner.GotoState(PlayerStateType.Diving);
        }
    }

    public override void Exit()
    {
        
    }
}
public class RollingState : PlayerState
{
    
    public override void Enter()
    {
        Debug.Log("Rolling");
    }

    public override void Update()
    {
        // if finished rolling  owner.GotoState(PlayerStateType.Normal);
    }

    public override void Exit()
    {

    }
}
public class DivingState : PlayerState
{
    public override void Enter()
    {
        Debug.Log("Diving");
        player.transform.Rotate(new Vector3(90, 0));
        
    }

    public override void Update()
    {
        //player.controller.Move(new Vector3(Input * Time.deltaTime, 0));

        player.velocity.y += player.gravity * Time.deltaTime;
        player.controller.Move(player.velocity * Time.deltaTime);

        if (player.grounded) owner.GotoState(PlayerStateType.Rolling);

        
    }

    public override void Exit()
    {

    }
}
