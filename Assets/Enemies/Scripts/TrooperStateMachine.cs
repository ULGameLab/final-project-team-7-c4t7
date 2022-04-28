using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TrooperStateMachine : MonoBehaviour
{

    public enum State { Idle,toIdle, KillLuke, Strafe, Patrol}
    public State CurState;
    private Transform Plyr;
    private UnityEngine.AI.NavMeshAgent AI;
    private NavMeshAgent NavAg;
    private Vector3 direction;

    private Dictionary<State, System.Action> enter;
    private Dictionary<State, System.Action> exit;
    private Dictionary<State, System.Action> execute;

    [Tooltip("default speed for navmeshes is 3.5")]
    private float SPD;
    public float walkSpeed = 3.5f;
    //GunTransform Variables
    public Transform FireGun;
    public Transform IdleGun;
    //Idle variables
    bool isIdle;
    Vector3 IPos;
    Vector3 IRot;

    //KillLuke variables
    [Tooltip("175 for troopers")]
    public float VisRng = 175;
    [Tooltip("distance to see luke, probably 25")]
    public float VisDist = 25;
    public AudioSource sounds;
    public AudioClip pew;
    bool KillOnce;
    Vector3 KPos;
    public Transform GunTipT;
    private float fireGun;
    public float whenToFire = 10000;
    private Animator animation;
    public float walkWhileShootingSpd = .5f;
    //GameObject SelectedPre;
    GameObject Fired;
    public GameObject BulletType;
    //public GameObject Rocket;
    //int rocketMult = 3;
    //float SelectedF;
    public float BlasterBoltF = 5.5f;
    private int numberOfBullets;
    
    

    //Patrol Variables
    public bool willPatrol = true;
    private bool onPatrol;//determine if to enter Patrol State
    public float idleTime = 5; //seconds there is before object starts to patrol from idle state;
    private float patrolTime;
    
    private Transform PatrolTarget;//nearest Patrol Way
    private Transform pTarget1; //child of Patrol or the first patrol point
    private Transform pTarget2; //child of Patrol or the 2nd patrol point
    private Transform pTarget;
    private Transform tempTarget;

    //Strafe Variables
    private float timeToMove = 5;
    public float strafeSpeed = 5;
    


    // Start is called before the first frame update
    void Start()
    {
        Plyr = GameObject.FindGameObjectWithTag("Player").transform;
        AI = GetComponent<UnityEngine.AI.NavMeshAgent>();
        patrolTime = idleTime;
        animation = this.GetComponent<Animator>();
        

        
        isIdle = true;
        IPos = transform.position;
        IRot = transform.forward;
        

        enter = new Dictionary<State, System.Action>()
        {
            {State.Idle, entI },
            {State.toIdle, enttI },
            {State.KillLuke, entK },
            {State.Strafe, entS },
            {State.Patrol,entP }
        };
        exit = new Dictionary<State, System.Action>()
        {
            {State.Idle, extI },
            {State.toIdle, exttI },
            {State.KillLuke, extK },
            {State.Strafe, extS },
            {State.Patrol,extP }

        };
        execute = new Dictionary<State, System.Action>()
        {
            {State.Idle, excI },
            {State.toIdle, exctI },
            {State.KillLuke, excK },
            {State.Strafe, excS },
            {State.Patrol,excP }

        };

        CurState = State.Idle;
    }

    

    void Update()
    {
        execute[CurState]();
    }
    private float GetMagnitude(Vector3 v)
    {
        return Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2) + Mathf.Pow(v.z, 2));
    }

    private Vector3 GetNormalized(Vector3 v)
    {
        return v / GetMagnitude(v);
    }
    private bool CanSee()
    {
        Vector3 toPlyr = Plyr.position - transform.position;
        float AglToPlyr = Vector3.Angle(transform.forward, toPlyr);
        return AglToPlyr < VisRng && Vector3.Distance(transform.position, Plyr.position) < 30;
    }

    private bool InRange()
    {
        return Vector3.Distance(transform.position, Plyr.position) < VisDist;
    }

    void Transition(State nextState)
    {
        exit[CurState]();
        CurState = nextState;
        enter[CurState]();
    }

    //State Behaviors
    //Idle: q
    void entI()
    {
        resetAnimation();
        patrolTime = idleTime;
        IdleGun.gameObject.SetActive(true);
        FireGun.gameObject.SetActive(false);
        
        
        
    }
    void extI()
    {
        isIdle = false;
        resetAnimation();
    }
    void excI()
    {
        if (patrolTime > -5)
        {
            InvokeRepeating("patrolTimeCountDown", 0f, 2f);
        }
        //if not there, return to idle possition, then stop checking the if statments
        if (!isIdle)
        {
            if (transform.position != IPos) {
                Transition(State.toIdle);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(IRot);
                isIdle = true;
            }
        }
        
        //Debug.Log(willPatrol + "  " + " " + patrolTime + " " + " " +onPatrol);
        if (willPatrol  && onPatrol)
        {
            Transition(State.Patrol);
        }
        

        if (InRange() && CanSee()/*Input.GetKey(KeyCode.W)*/)//change to can see luke later
        {
            Transition(State.KillLuke);
        }

    }
    void patrolTimeCountDown()
    {
        patrolTime -= 1 * Time.deltaTime;
    }
    //toIdle 

    void enttI()
    {
        animation.SetBool("isWalk", true);
        patrolTime = 0;
    }
    void exttI()
    {
        resetAnimation();
    }
    void exctI()
    {
        AI.destination = IPos;
        if ((transform.position - IPos).magnitude < 2)
        {
            Transition(State.Idle);
        }
        

        //Debug.Log("execute Idle")
        if (InRange() && CanSee()/*Input.GetKey(KeyCode.W)*/)//change to can see luke later
        {
            Transition(State.KillLuke);
        }

    }
    //KillLuke: w
    void entK()
    {
        KillOnce = true;
        SPD = walkWhileShootingSpd;
        animation.SetBool("isFire", true);
        IdleGun.gameObject.SetActive(false);
        FireGun.gameObject.SetActive(true);
    }
    void extK()
    {
        resetAnimation();
    }
    void excK()
    {

        
        fireGun += Random.Range(0, 100);
        //circle luke
        if (KillOnce)
        {
            
            KillOnce = false;
        }

        //shoot luke
        
        transform.LookAt(Plyr.position);

        // Initiate Bullet
        //if (Input.GetKeyDown(KeyCode.E))
        
        if (fireGun > whenToFire)
        {
            Fire();
            timeToMove--;
            if (Random.Range(0, 3) == 0)
                fireGun = fireGun - (whenToFire /10);
            else
                fireGun = fireGun - whenToFire;

        }

        if (timeToMove < 1)
        {
            Transition(State.Strafe);
        }

        //transitions
        //Debug.Log("execute KillLuke");
        if (!CanSee()/*Input.GetKey(KeyCode.Q)*/)//replace with luke being out of signt
        {
            if (onPatrol)
            {
                Transition(State.Patrol);
            }
            else
            {
                Transition(State.Idle);
            }
        }
    }
    
    
    void Fire()
    {
        sounds.PlayOneShot(pew);
        Fired = Instantiate(BulletType,GunTipT.position,Quaternion.identity);
    
        Fired.GetComponent<Rigidbody>().mass = .2f;
        
        Vector3 bulletDirection = Plyr.position - Fired.transform.position;
        Fired.transform.forward = bulletDirection.normalized;

        Fired.GetComponent<Rigidbody>().useGravity = false;
        Fired.GetComponent<Rigidbody>().angularDrag = 0;
        
        Fired.GetComponent<Rigidbody>().AddForce(bulletDirection * 1f, ForceMode.Impulse);
    }
    //Strafe

    void entS()
    {
    
        animation.SetBool("isRun", true);
        SPD = strafeSpeed;
        Vector2 tempv2 = Random.insideUnitCircle;
        KPos = (Plyr.transform.position + (Random.Range(7, 13) * Plyr.transform.forward) + (Random.Range(6, 12) * new Vector3(tempv2.x, 0f, tempv2.y)));//Roberto changed this It add a bit more randoness to the behavior
        AI.destination = KPos;
        
    }
    void extS()
    {
        resetAnimation();
        timeToMove = Random.Range(2, 5);
    }
    void excS()
    {
        transform.LookAt(KPos);
        if (InRange() && CanSee()/*Input.GetKey(KeyCode.W)*/)//change to can see luke later
        {
            Transition(State.KillLuke);
        }
        if (!CanSee()/*Input.GetKey(KeyCode.Q)*/)//replace with luke being out of signt
        {
            Transition(State.Idle);
        }
    }


    //Patrol
    void entP()
    {
        SPD = walkSpeed;
        pTarget1 = PatrolTarget.GetChild(0);
        pTarget2 = PatrolTarget.GetChild(1);
        pTarget = tempTarget;
        animation.SetBool("isWalk", true);
    }
    void extP()
    {
        resetAnimation();
        tempTarget = pTarget;
    }
    void excP()
    {
        direction = GetNormalized(pTarget.position - transform.position);
        AI.destination = pTarget.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                            Quaternion.LookRotation(direction), 0.01f);
      
        //Debug.Log((transform.position - pTarget.transform.position).magnitude);
        if (Mathf.Abs((transform.position - pTarget.transform.position).magnitude) < 1)
        {
            if (pTarget == PatrolTarget.GetChild(0)) { pTarget = PatrolTarget.GetChild(1); }
            else if (pTarget == PatrolTarget.GetChild(1)) { pTarget = PatrolTarget.GetChild(0); }
        }

        if (InRange() && CanSee()/*Input.GetKey(KeyCode.W)*/)//change to can see luke later
        {
            Transition(State.KillLuke);
        }

    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PatrolTarget")
        {
            PatrolTarget = other.transform;
            tempTarget = PatrolTarget.GetChild(0);
            onPatrol = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PatrolTarget")
        {
            onPatrol = false;
        }
    }
    void resetAnimation()
    {
        animation.SetBool("isRun", false);
        animation.SetBool("isFire", false);
        animation.SetBool("isWalk", false);
    }

    
}
