using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static Enemy instance;
    public static Enemy Instance
    {
        get
        {
            return instance;
        }
    }

    private EnemyFSM fsm;
    public GameObject projectile;
    internal bool finishedFiring = true;

    internal Animator animator;

    private void Awake()
    {
        instance = this;
        fsm = new EnemyFSM();
        fsm.Initialize(this);

        fsm.AddState(EnemyStateType.Idle, new IdleState());
        fsm.AddState(EnemyStateType.Sweep, new SweepState());
        fsm.AddState(EnemyStateType.Slam, new SlamState());
        fsm.AddState(EnemyStateType.Rain, new RainState());

        fsm.GotoState(EnemyStateType.Idle);
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    public EnemyStateType GetRandomState()
    {
        EnemyStateType e = (EnemyStateType)Random.Range(1, 4);
        Debug.Log(e);
        return e;
    }

    public IEnumerator SpawnProjectiles(int num, float frequency)
    {
        for (int i=0; i<num; i++)
        {
            
            GameObject p = Instantiate(projectile);
            p.transform.position = new Vector3(Random.Range(-15.0f, 15.0f), 15.0f, -10f);
            yield return new WaitForSeconds(frequency);
        }
        finishedFiring = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Player.Instance.TakeDamage();
        }
    }
}
