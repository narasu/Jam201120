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


    public GameObject shockwave;

    

    internal Animator animator;

    private void Awake()
    {
        instance = this;

        animator = GetComponent<Animator>();

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
        return e;
    }

    public IEnumerator SpawnProjectiles(int num, float frequency)
    {
        for (int i=0; i<num; i++)
        {
            
            GameObject p1 = Instantiate(projectile);
            p1.transform.position = new Vector3(Random.Range(-15.0f, 15.0f), 15.0f, -12.5f);
            GameObject p2 = Instantiate(projectile);
            p2.transform.position = new Vector3(Random.Range(-15.0f, 15.0f), 15.0f, -12.5f);
            yield return new WaitForSeconds(frequency);
        }
        finishedFiring = true;
    }

    public void SpawnShockwaves()
    {
        GameObject left = Instantiate(shockwave);
        left.transform.position = new Vector3(-1f, 1f, -12.5f);
        Shockwave sl = left.GetComponent<Shockwave>();
        sl.rb.velocity = Vector3.left * sl.speed;

        GameObject right = Instantiate(shockwave);
        right.transform.position = new Vector3(1f, 1f, -12.5f);
        Shockwave sr = right.GetComponent<Shockwave>();
        sr.rb.velocity = Vector3.right * sr.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Player.Instance.TakeDamage();
        }
    }
}
