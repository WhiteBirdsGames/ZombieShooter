using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour
{
    public float HP = 100;
    public Transform PlayerTrans;
    public float DistanceAttack, SpeedRunZombie, AttackDamage;
    public enum States
    {
        Idle,
        Run,
        Damage,
        Slaps,
        Death
    }
    public States State;
    public Animator ZombieAnimator;
    public bool Run, Damage, Death;
    public int Slap;

    public NavMeshAgent navMeshAgent;
    public Collider[] CollidersDisableDeath;

    private void Awake()
    {
        navMeshAgent.autoBraking = false;
        navMeshAgent.stoppingDistance = DistanceAttack;
        navMeshAgent.speed = SpeedRunZombie;
    }

    private void Update()
    {
        if(State == States.Run)
        {
            RunToPlyer();
        }

        if (State == States.Slaps)
        {
            AttackPlayer();
        }
        if (State == States.Death)
        {
            navMeshAgent.isStopped = true;
            Run = false;
            Damage = false;
            Slap = 0;
            Death = true;
        }

        if (Death)
        {
            State = States.Death;
        }
    }

    public void OnDamagePlayer ()
    {
        if (Vector3.Distance(transform.position, PlayerTrans.position) <= DistanceAttack)
        {
            PlayerTrans.GetComponent<WeaponsControl>().DamagePlayer(AttackDamage);
        }
    }

    public void DamageZombie (float _damage)
    {
        State = States.Damage;
        navMeshAgent.isStopped = true;
        Run = false;
        Damage = true;
        Slap = 0;
        HP -= _damage;
        if(HP <= 0)
        {
            foreach(Collider col in CollidersDisableDeath)
            {
                col.enabled = false;
            }
            State = States.Death;
        }
    }

    public void EndDamage ()
    {
        Damage = false;
        State = States.Run;
    }

    public void AttackPlayer()
    {
        navMeshAgent.isStopped = true;
        Run = false;
        Damage = false;
        Slap = 1;

        if (Vector3.Distance(transform.position, PlayerTrans.position) >  DistanceAttack + 0.4f)
        {
            State = States.Run;
        }
    }

    public void RunToPlyer ()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(PlayerTrans.position);
        Run = true;
        Damage = false;
        Slap = 0;

        if(Vector3.Distance(transform.position, PlayerTrans.position) <= DistanceAttack)
        {
            State = States.Slaps;
        }
    }

    private void FixedUpdate()
    {
        ZombieAnimator.SetBool("Run", Run);
        ZombieAnimator.SetBool("Damage", Damage);
        ZombieAnimator.SetBool("Death", Death);
        ZombieAnimator.SetInteger("Slap", Slap);
    }

   

}
