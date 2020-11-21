using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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


    private void Awake()
    {
        fsm = new PlayerFSM();
        fsm.Initialize(this);
        fsm.AddState(PlayerStateType.Normal, new NormalState());
        fsm.AddState(PlayerStateType.Rolling, new RollingState());
        fsm.AddState(PlayerStateType.Diving, new DivingState());
        
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        fsm.GotoState(PlayerStateType.Normal);
    }

    void Update()
    {
        fsm.UpdateState();
        
        //grounded = controller.isGrounded;
        //if (grounded && velocity.y < 0)
        //{
        //    velocity.y = 0f;
        //}

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}
        //controller.Move(move * Time.deltaTime * speed);

        //if (Input.GetButtonDown("Jump") && grounded)
        //{
        //    Debug.Log("jumping");
        //    velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        //}

        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }
}
