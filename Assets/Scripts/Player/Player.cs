using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    PlayerFSM fsm;
    
    internal CharacterController controller;
    internal Vector3 velocity;
    internal bool grounded;
    public float speed = 8.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;

    public float dashSpeed = 16.0f;
    public float dashTime = 0.2f;
    public float dashCooldown = 0.2f;
    internal float dashTimer = 0f;
    internal bool canDash = true;
    internal float dashDirection = 1;

    public int health = 3;

    internal Animator animator;

    private void Awake()
    {
        instance = this; 
        fsm = new PlayerFSM();
        fsm.Initialize(this);
        fsm.AddState(PlayerStateType.Normal, new NormalState());
        fsm.AddState(PlayerStateType.Dash, new DashState());
        
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        fsm.GotoState(PlayerStateType.Normal);
    }

    void Update()
    {
        fsm.UpdateState();

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
        else if (grounded)
        {
            canDash = true;
        }
            
        
    }
    public void TakeDamage()
    {
        health--;
        Debug.Log("Health: " + health);
        if (health == 0) Die();
    }

    private void Die()
    {
        Debug.Log("dead");
         SceneManager.LoadScene("Death");
        // ded
        // go to dead screen
    }
}
