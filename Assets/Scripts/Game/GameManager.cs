using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public float startTime;
    [HideInInspector] public float timer;

    public GameObject door;
    public Text healthText;

    private GameFSM fsm;

    private void Awake()
    {
        instance = this;
        fsm = new GameFSM();
        fsm.Initialize(this);

        fsm.AddState(GameStateType.Play, new PlayState());
        fsm.AddState(GameStateType.Win, new WinState());
        fsm.AddState(GameStateType.Dead, new DeadState());

        fsm.GotoState(GameStateType.Play);

        // level timer
        timer = startTime;
    }

    private void Update()
    {
        fsm.UpdateState();
        healthText.text = "Health: " + Player.Instance.health;
    }
}
