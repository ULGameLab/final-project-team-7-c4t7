using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackTrooperStateMachine : MonoBehaviour
{
    public enum State { Idle, KillLuke, Patrol,Punch }
    public State CurState;
    private Transform Plyr;
    private UnityEngine.AI.NavMeshAgent AI;
    private NavMeshAgent NavAg;
    private Vector3 direction;

    private Dictionary<State, System.Action> enter;
    private Dictionary<State, System.Action> exit;
    private Dictionary<State, System.Action> execute;

    [Tooltip("default speed for navmeshes is 3.5")]
    public float SPD;

    //Idle variables
    bool isIdle;
    Vector3 IPos;
    Vector3 IRot;

    //KillLuke variables
    [Tooltip("175 for troopers")]
    public float VisRng = 175;
    [Tooltip("distance to see luke, probably 25")]
    public float VisDist = 25;
    //public AudioSource sounds;
    //public AudioClip pew;
    bool KillOnce;
    Vector3 KPos;
    public Transform GunTipT;
    //GameObject SelectedPre;
    GameObject Fired;
    public GameObject BulletType;
    //public GameObject Rocket;
    //int rocketMult = 3;
    //float SelectedF;
    //public float BlasterBoltF = 5.5f;
    //public float RocketF = 1.7f;

    private float fireGun;
    public float whenToFire = 10000;

    //Patrol Variables
    public bool willPatrol;
    private bool onPatrol;//determine if to enter Patrol State
    public float idleTime = 5; //seconds there is before object starts to patrol from idle state;
    private float patrolTime;

    private Transform PatrolTarget;//nearest Patrol Way
    private Transform pTarget1; //child of Patrol or the first patrol point
    private Transform pTarget2; //child of Patrol or the 2nd patrol point
    private Transform pTarget;
    private Transform tempTarget;

    //Punch Variables
    public float punchDistance = 2;


    // Start is called before the first frame update
    void Start()
    {
        Plyr = GameObject.FindGameObjectWithTag("Player").transform;
        AI = GetComponent<UnityEngine.AI.NavMeshAgent>();
        patrolTime = idleTime;


        isIdle = true;
        IPos = transform.position;
        IRot = transform.forward;

        enter = new Dictionary<State, System.Action>()
        {
            {State.Idle, entI },
            {State.KillLuke, entK },
            {State.Patrol,entP },
            {State.Punch,entPu }
        };
        exit = new Dictionary<State, System.Action>()
        {
            {State.Idle, extI },
            {State.KillLuke, extK },
            {State.Patrol,extP },
            {State.Punch,extPu }

        };
        execute = new Dictionary<State, System.Action>()
        {
            {State.Idle, excI },
            {State.KillLuke, excK },
            {State.Patrol,excP },
            {State.Punch,excPu }

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
        patrolTime = idleTime;
    }
    void extI()
    {
        isIdle = false;
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
            if (transform.position != IPos) { AI.destination = IPos; }
            else
            {
                transform.rotation = Quaternion.LookRotation(IRot);
                isIdle = true;
            }
        }
        //Debug.Log(willPatrol + "  " + " " + patrolTime + " " + " " +onPatrol);
        if (willPatrol && patrolTime < 0 && onPatrol)
        {
            Transition(State.Patrol);
        }

        //Debug.Log("execute Idle")
        if (InRange() && CanSee()/*Input.GetKey(KeyCode.W)*/)//change to can see luke later
        {
            Transition(State.KillLuke);
        }

    }
    void patrolTimeCountDown()
    {
        patrolTime -= 1 * Time.deltaTime;
    }

    //KillLuke: w
    void entK()
    {
        KillOnce = true;
    }
    void extK()
    {
        StopCoroutine("Strafe");
    }
    void excK()
    {

        fireGun += Random.Range(0, 100);
        //circle luke
        if (KillOnce)
        {
            InvokeRepeating("Strafe", 0f, 8f);//Roberto changed this seens to work kind of better with A high repetitiong
            KillOnce = false;
        }

        //shoot luke

        transform.LookAt(Plyr.position);
        if (fireGun > whenToFire)
        {
            Fire();
            if (Random.Range(0, 3) == 0)
                fireGun = fireGun - (whenToFire / 10);
            else
                fireGun = fireGun - whenToFire;
        }
        //transitions
        //Debug.Log("execute KillLuke");
        if (!CanSee()/*Input.GetKey(KeyCode.Q)*/)//replace with luke being out of signt
        {
            if (onPatrol) { Transition(State.Patrol); }
            else
                Transition(State.Idle);
        }

        if (Mathf.Abs((Plyr.position - transform.position).magnitude) < punchDistance)
        {
            Transition(State.Punch);
        }
    }
    void Strafe()
    {
        Vector2 tempv2 = Random.insideUnitCircle;
        KPos = (Plyr.transform.position + (Random.RandomRange(7, 13) * Plyr.transform.forward) + (Random.Range(6, 12) * new Vector3(tempv2.x, 0f, tempv2.y)));//Roberto changed this It add a bit more randoness to the behavior
        AI.destination = KPos;
        transform.LookAt(KPos);

    }
    void Fire()
    {
        //sounds.PlayOneShot(pew);
        Fired = Instantiate(BulletType, GunTipT.position, Quaternion.identity);

        Fired.GetComponent<Rigidbody>().mass = .2f;

        Vector3 bulletDirection = Plyr.position - Fired.transform.position;
        Fired.transform.forward = bulletDirection.normalized;

        Fired.GetComponent<Rigidbody>().useGravity = false;
        Fired.GetComponent<Rigidbody>().angularDrag = 0;

        Fired.GetComponent<Rigidbody>().AddForce(bulletDirection * 1f, ForceMode.Impulse);
    }

    //Patrol
    void entP()
    {
        pTarget1 = PatrolTarget.GetChild(0);
        pTarget2 = PatrolTarget.GetChild(1);
        pTarget = tempTarget;
    }
    void extP()
    {
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
    //Punch State
    void entPu()
    {

    }
    void extPu()
    {

    }
    void excPu()
    {

        if (Mathf.Abs((Plyr.position - transform.position).magnitude) > punchDistance)
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
        else
            onPatrol = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PatrolTarget")
        {
            onPatrol = false;
        }
    }
    
}
