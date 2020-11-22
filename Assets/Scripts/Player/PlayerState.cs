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

        if (player.grounded)
        {
            if (player.velocity.y < 0)
            {
                player.velocity.y = 0f;
            }

            player.animator.SetBool("isOnGround", true);
        }
        else
        {
            player.animator.SetBool("isOnGround", false);
        }

        if (Input.GetAxisRaw("Horizontal") != 0) player.dashDirection = Input.GetAxisRaw("Horizontal");



        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (move != Vector3.zero)
        {
            player.transform.forward = move;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            player.animator.SetBool("isMoving", true);
        }
        else if (move==Vector3.zero)
        {
            player.animator.SetBool("isMoving", false);
        }
        
        ctrl.Move(move * Time.deltaTime * player.speed);

        if (Input.GetButtonDown("Jump") && player.grounded)
        {
            Debug.Log("jumping");
            player.velocity.y += Mathf.Sqrt(player.jumpHeight * -3.0f * player.gravity);
        }
        player.velocity.y += player.gravity * Time.deltaTime;
        ctrl.Move(player.velocity * Time.deltaTime);
        
        /*
        if (Input.GetButtonDown("Dodge") && player.canDash)
        {
            
            owner.GotoState(PlayerStateType.Dash);
        }*/
    }

    public override void Exit()
    {
        player.velocity.y = 0;
    }
}
public class DashState : PlayerState
{
    

    float t = 0f;
    float dir;

    public override void Enter()
    {
        Debug.Log("Dashing");
        player.animator.SetBool("isDashing", true);
    }

    public override void Update()
    {
        if(t < player.dashTime)
        {
            player.controller.Move(new Vector3(player.dashDirection * player.dashSpeed * Time.deltaTime, 0));
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
            owner.GotoState(PlayerStateType.Normal);
        }
    }


    public override void Exit()
    {
        player.canDash = false;
        player.dashTimer = player.dashCooldown;

        player.animator.SetBool("isDashing", false);
    }

    public void SetDirection(float _dir) => dir = _dir;
}
