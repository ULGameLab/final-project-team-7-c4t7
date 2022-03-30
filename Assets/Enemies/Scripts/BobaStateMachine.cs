using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class BobaStateMachine : MonoBehaviour
{
    public enum State { Idle, KillLuke, Fly }
    public State CurState;

    private Transform Plyr;
    private UnityEngine.AI.NavMeshAgent AI;
    private NavMeshAgent NavAg;

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
    [Tooltip("255 for boba")]
    public float VisRng = 225;
    [Tooltip("distance to see luke, probably 25")]
    public float VisDist = 25;
    //public AudioSource sounds;
    //public AudioClip pew;
    bool KillOnce;
    Vector3 KPos;
    public Transform GunTipT;
    //GameObject SelectedPre;
    GameObject Fired;
    private GameObject BulletType;
    public GameObject BlastetBolt;
    public GameObject Rocket;
    //int rocketMult = 3;
    //float SelectedF;
    //public float BlasterBoltF = 5.5f;
    //public float RocketF = 1.7f;
    private float fireGun;
    public float whenToFire = 10000;
    //Fly variables
    Vector3 FPos;

    private Vector3 tempPos;

    public float flyUp = 5;
    public float flyTime = 10;
    

    int JmpTme = 0;
    void Start()
    {
        Plyr = GameObject.FindGameObjectWithTag("Player").transform;
        AI = GetComponent<UnityEngine.AI.NavMeshAgent>();


        isIdle = true;
        IPos = transform.position;
        IRot = transform.forward;



        enter = new Dictionary<State, System.Action>()
        {
            {State.Idle, entI },
            {State.KillLuke, entK },
            {State.Fly, entF }
        };
        exit = new Dictionary<State, System.Action>()
        {
            {State.Idle, extI },
            {State.KillLuke, extK },
            {State.Fly, extF }
        };
        execute = new Dictionary<State, System.Action>()
        {
            {State.Idle, excI },
            {State.KillLuke, excK },
            {State.Fly, excF }
        };

        CurState = State.Idle;
    }

    // Update is called once per frame
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

    }
    void extI()
    {
        isIdle = false;
    }
    void excI()
    {
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
        flyUp = 5;
        BulletType = BlastetBolt;
        

    }
    void extK()
    {
        StopCoroutine("Strafe");
        StopCoroutine("FlyUp");
    }
    void excK()
    {
        //math on the random actions that enemies can take
        //JmpTme = (JmpTme + Random.Range(0, 3)) % 550;
        //if (JmpTme > 385 && isBoba) { SelectedPre = Rocket; SelectedF = RocketF; }
        //else { SelectedPre = BlastetBolt; SelectedF = BlasterBoltF; }
        InvokeRepeating("FlyUp", 0f, 2f);
        fireGun += Random.Range(0, 100);
        //circle luke
        if (KillOnce)
        {
            InvokeRepeating("Strafe", 0f, 8f);//Roberto changed this seens to work kind of better with A high repetitiong
            KillOnce = false;
        }

        //shoot luke
        transform.LookAt(Plyr.transform);

        if (fireGun > whenToFire)
        {
            Fire();
            if (Random.Range(0, 3) == 0)
                fireGun = fireGun - (whenToFire / 10);
            else
                fireGun = fireGun - whenToFire;
        }
        //instantiate bullets here
        //if (SelectedPre == Rocket) { if (JmpTme % 70 * rocketMult == 0) { Fire(); } }
        //else { if (JmpTme % 70 == 0) { Fire(); } }

        //transitions
        //Debug.Log("execute KillLuke");
        if (!CanSee()/*Input.GetKey(KeyCode.Q)*/)//replace with luke being out of signt
        {
            Transition(State.Idle);
        }
        if (flyUp < 0)
        {
            Transition(State.Fly);
        }
    }
    void Strafe()
    {
        Vector2 tempv2 = Random.insideUnitCircle;
        KPos = (Plyr.transform.position + (Random.RandomRange(7, 13) * Plyr.transform.forward) + (Random.Range(6, 12) * new Vector3(tempv2.x, 0f, tempv2.y)));//Roberto changed this It add a bit more randoness to the behavior
        AI.destination = KPos;
    }
    void FlyUp()
    {
        flyUp -= 1 * Time.deltaTime;
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

    //F: e
    void entF()
    {
        flyTime = 10;
        BulletType = Rocket;
        
    }
    void extF()
    {
        StopCoroutine("FlyCountDown");
    }
    void excF()
    {

        //auto transition, its in the coroutine
        //big jetpack jump
        StartCoroutine(JetpackJump());
        InvokeRepeating("FlyCountDown", 0f, 2f);
        Vector3 direction = Plyr.position - transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                            Quaternion.LookRotation(direction), 0.1f);

        if (fireGun > whenToFire)
        {
            Fire();
            if (Random.Range(0, 3) == 0)
                fireGun = fireGun - (whenToFire / 10);
            else
                fireGun = fireGun - whenToFire;
        }

        if (flyTime > 0)
            StartCoroutine(JetpackJump());
        else if (flyTime < 0)
        {
            if (InRange() && CanSee())
                Transition(State.KillLuke);
            else
                Transition(State.Idle);
        }
        //else
        //{
        //    StartCoroutine(FlyDown());
        //    if (InRange() && CanSee())
        //        Transition(State.KillLuke);
        //    else
        //        Transition(State.Idle);
        //}
    }
    
    IEnumerator JetpackJump()//the jetpack jump boba does.
    {
        float tempf = 0;
        AI.speed = SPD * 8;
        while (tempf < 257)
        {
            transform.position += new Vector3(0, 0.017f, 0f);
            tempf += 1;
            yield return new WaitForSecondsRealtime(.001f);
        }
        AI.speed = SPD;
        
    }

    void FlyCountDown()
    {
        flyTime -= 1 * Time.deltaTime;
    }
    
}
