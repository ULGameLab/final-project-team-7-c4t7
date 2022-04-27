using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StormTrooperStateMachine : MonoBehaviour
{
    //NavMesh
    private UnityEngine.AI.NavMeshAgent AI;
    private NavMeshAgent NavAg;

    //States
    public enum State { Idle, Chase, Shoot, Strafe, Patrol }
    public State CurState;
    private Dictionary<State, System.Action> enter;
    private Dictionary<State, System.Action> exit;
    private Dictionary<State, System.Action> execute;
    
        //Patrol
    private Transform PatrolTarget,pTarget,pTarget1,pTarget2;
    private bool onPatrol = false;
    private bool gonnaTurn = false;


    //Player
    private Transform Plyr;

    //Entity
    [Tooltip("Entity Stats")]
    private float VisRng = 110;
    private float shootDistance = 15;
    private float VisDist = 25;
    public float walkSpeed = 3.5f;
    public float runSpeed = 7f;
    public float shootSpeed = 1f;

    //Bullets and Guns
    public Transform gunTip;
    GameObject bulletsFired;
    public GameObject bulletType;
    private float fireGun;
    private float whenToFire = 5000;

    //Sounds
    public AudioSource sounds;
    public AudioClip pew;

    //Animation
    [Tooltip("AnimationStats")]
    public StormTrooperAnimationScript theState;


    // Start is called before the first frame update
    void Start()
    {
        //initialization
        Plyr = GameObject.FindGameObjectWithTag("Player").transform;
        theState = this.gameObject.GetComponent<StormTrooperAnimationScript>();//animation States: Idle = 0,Walk = 1, Run = 2, Shoot = 3
        NavAg = GetComponent<NavMeshAgent>();


        enter = new Dictionary<State, System.Action>()
        {
            {State.Idle, enterI },
            {State.Chase, enterC },
            {State.Shoot, enterS },
            {State.Patrol, enterP },
        };
        exit = new Dictionary<State, System.Action>()
        {
            {State.Idle, exitI },
            {State.Chase, exitC },
            {State.Shoot, exitS },
            {State.Patrol, exitP },

        };
        execute = new Dictionary<State, System.Action>()
        {
            {State.Idle, executeI },
            {State.Chase, executeC },
            {State.Shoot, executeS },
            {State.Patrol, executeP },

        };

        CurState = State.Idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        execute[CurState]();
    }

    void Transition(State nextState)
    {
        exit[CurState]();
        CurState = nextState;
        enter[CurState]();
    }

    //Entity interact with Player
    private bool CanSee()
    {
        Vector3 toPlyr = Plyr.position - transform.position;
        float AglToPlyr = Vector3.Angle(transform.forward, toPlyr);
        return AglToPlyr < VisRng && Vector3.Distance(transform.position, Plyr.position) < 30;
    }
    private bool InRangeToChase()
    {
        return Vector3.Distance(transform.position, Plyr.position) < VisDist && Vector3.Distance(transform.position, Plyr.position) > shootDistance;
    }
    private bool InRangeToShoot()
    {
        return Vector3.Distance(transform.position, Plyr.position) < VisDist && Vector3.Distance(transform.position, Plyr.position) < shootDistance;
    }

    //Idle/////////////////////////////////////////////////////////////////
    void enterI()
    {
        theState.animationState = 0;
    }
    void exitI()
    {

    }
    void executeI()
    {
        if (CanSee())
        {
            if (InRangeToChase())
                Transition(State.Chase);    
            if (InRangeToShoot())
                Transition(State.Shoot);
        }
        if (onPatrol)
            Transition(State.Patrol);
    }

    //Chase/////////////////////////////////////////////////////////////////
    void enterC()
    {
        theState.animationState = 2;
        NavAg.speed = runSpeed;
    }
    void exitC()
    {

    }
    void executeC()
    {
        NavAg.destination = Plyr.position;
        if (InRangeToShoot() && CanSee())
            Transition(State.Shoot);

        if (!CanSee())
            Transition(State.Idle);

    }
    //Shoot/////////////////////////////////////////////////////////////////
    void enterS()
    {
        theState.animationState = 3;
        fireGun = whenToFire;
        NavAg.speed = shootSpeed;
    }
    void exitS()
    {
    }
    void executeS()
    {
        NavAg.destination = Plyr.position;
        fireGun += Random.Range(0, 100);
        Debug.Log(fireGun);
        if (fireGun >= whenToFire)
        {
            Fire();
            if (Random.Range(0, 3) == 0)
            {
                fireGun = 0;
                fireGun = whenToFire - (whenToFire / 10);
            }
            else
            {
                fireGun = 0;
            }
        }
        
        
        

        if (!InRangeToShoot() && CanSee())
            Transition(State.Chase);
        if (!CanSee())
            Transition(State.Idle);
    }

    void Fire()
    {
        sounds.PlayOneShot(pew);
        bulletsFired = Instantiate(bulletType, gunTip.position, Quaternion.identity);

        bulletsFired.GetComponent<Rigidbody>().mass = .2f;

        Vector3 bulletDirection = Plyr.position - bulletsFired.transform.position;
        bulletsFired.transform.forward = bulletDirection.normalized;

        bulletsFired.GetComponent<Rigidbody>().useGravity = false;
        bulletsFired.GetComponent<Rigidbody>().angularDrag = 0;

        bulletsFired.GetComponent<Rigidbody>().AddForce(bulletDirection * 1f, ForceMode.Impulse);
    }
    
    
    //Strafe//////////////////////////////////////////////////////////////
    
    //Patrol/////////////////////////////////////////////////////////////////
    void enterP()
    {
        theState.animationState = 1;
        NavAg.speed = walkSpeed;
        pTarget1 = PatrolTarget.Find("Patrol2");
        pTarget2 = PatrolTarget.Find("Patrol1");
        NavAg.SetDestination(pTarget.position);

    }
    void exitP()
    {
        onPatrol = false;
    }
    void executeP()
    {
        if (gonnaTurn)
        {
            pTarget = pTarget2;
            NavAg.destination = pTarget.position;
        }
        else
        {
            pTarget = pTarget1;
            NavAg.destination = pTarget.position;
        }
        
        
        if (Vector3.Distance(transform.position,pTarget.transform.position) < 2)
        {
            if (pTarget == pTarget1)
                gonnaTurn = true;
            
            else if (pTarget == pTarget2)
                gonnaTurn = false;
        }
        


        if (CanSee())
        {
            if (InRangeToChase())
                Transition(State.Chase);
            if (InRangeToShoot())
                Transition(State.Shoot);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PatrolTarget")
        {
            PatrolTarget = other.transform;
            pTarget = PatrolTarget.Find("Patrol1");
            onPatrol = true;
        }
        
    }
}
