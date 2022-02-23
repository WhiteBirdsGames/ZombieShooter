using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ZombieControl : MonoBehaviour
{
    public float HP = 100;
    public bool IsFindPlayerStart = true;
    public Transform PlayerTrans;
    public float DistanceAttack, SpeedRunZombie, AttackDamage;


    [SerializeField]
    private UnityEvent _deathZombieEvent = default;
    [SerializeField]
    private float _timeToStartDeathEvent = default;

    public Rigidbody[] PartRigidbody;


    private bool isDeathEvent = false;

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
    public bool Run, Damage;
    public int Slap, Death;

    public NavMeshAgent navMeshAgent;
    public Collider[] CollidersDisableDeath;

    public float ForceKickDead;

    private void Awake()
    {
        navMeshAgent.autoBraking = false;
        navMeshAgent.stoppingDistance = DistanceAttack;
        navMeshAgent.speed = SpeedRunZombie;

        if(IsFindPlayerStart)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
                PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if(PlayerTrans == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return;
        }
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
          //  Death = Random.Range(1, 3);
            DeathEvent();
        }

        if (Death != 0)
        {
            State = States.Death;
        }
    }

    private void DeathEvent()
    {
        if (!isDeathEvent)
        {
            isDeathEvent = true;
            Invoke(nameof(StartDeathEvent), _timeToStartDeathEvent);
        }
    }

    private void StartDeathEvent()
    {
        _deathZombieEvent?.Invoke();
    }
    public void OnDamagePlayer ()
    {
        if (Vector3.Distance(transform.position, PlayerTrans.position) <= DistanceAttack)
        {
            PlayerTrans.GetComponent<WeaponsControl>().DamagePlayer(AttackDamage);
        }
    }

    public void DamageZombie (float _damage, Vector3 directionBullet, Rigidbody chestRigitbody)
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
            if(State != States.Death)
            {
                EnableRadgoll();
                chestRigitbody.AddForce(directionBullet * ForceKickDead);
                WavesZombieSpawner.Instance.AddKilledZombie();
            }
            State = States.Death;
        }
    }

    public void EnableRadgoll ()
    {
        ZombieAnimator.enabled = false;
        for (int i = 0; i < PartRigidbody.Length; i++)
        {
            PartRigidbody[i].isKinematic = false;
        }

        Invoke("DisableCollides", 2f);
    }

    public void DisableCollides ()
    {
        for (int i = 0; i < PartRigidbody.Length; i++)
        {
            PartRigidbody[i].isKinematic = true;
            PartRigidbody[i].GetComponent<Collider>().enabled = false;
        }
    }

    public void SetDead(Vector3 directionBullet, Rigidbody chestRigitbody)
    {
        if (State != States.Death)
        {
            EnableRadgoll();
            chestRigitbody.AddForce(directionBullet * ForceKickDead);
            WavesZombieSpawner.Instance.AddKilledZombie();
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
        Slap = Random.Range(1, 3);

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
       // ZombieAnimator.SetInteger("DeadVars", Death);
        ZombieAnimator.SetInteger("Slap", Slap);
    }

   

}
