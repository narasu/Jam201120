using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType { Normal, Dash }

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

        if (Input.GetButtonDown("Dodge") && player.canDash) owner.GotoState(PlayerStateType.Dash);
    }

    public override void Exit()
    {
        player.velocity.y = 0;
    }
}
public class DashState : PlayerState
{
    
    float t = 0f;

    public override void Enter()
    {
        Debug.Log("Dashing");
    }

    public override void Update()
    {
        if(t < player.dashTime)
        {
            player.controller.Move(new Vector3(Input.GetAxisRaw("Horizontal") * player.dashSpeed * Time.deltaTime, 0));
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
            owner.GotoState(PlayerStateType.Normal);
        }
        //player.velocity.y += player.gravity * Time.deltaTime;

        

    }


    public override void Exit()
    {
        player.canDash = false;
        player.dashTimer = player.dashCooldown;
    }
}
