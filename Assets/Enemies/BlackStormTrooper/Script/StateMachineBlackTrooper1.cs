using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateMachineBlackTrooper1 : MonoBehaviour
{
    //NavMesh
    private UnityEngine.AI.NavMeshAgent AI;
    private NavMeshAgent NavAg;

    //States
    public enum State { Idle, Chase, Shoot, Strafe, Patrol, Punch }
    public State CurState;
    private Dictionary<State, System.Action> enter;
    private Dictionary<State, System.Action> exit;
    private Dictionary<State, System.Action> execute;
    //Strafe
    private int timeToStrafe;
    private Vector3 StrafeDestination;
    private float xRandom = -7, zRandom = -7;
    private int xNegative, zNegative;
    private int backToShooting = 5;
    //Patrol
    private Transform PatrolTarget, pTarget, pTarget1, pTarget2;
    private bool onPatrol = false;
    private bool gonnaTurn = false;
    //Punch
    public GameObject punchPoint;
    public KnockBackPlayer thePunch;


    //Player
    private Transform Plyr;
    public LayerMask player;

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
    public float whenToFire = 5000;

    //Sounds
    public AudioSource sounds;
    public AudioClip pew;

    //Animation
    [Tooltip("AnimationStats")]
    public AnimationScriptBlackTrooper theState;


    // Start is called before the first frame update
    void Start()
    {
        //initialization
        Plyr = GameObject.FindGameObjectWithTag("Player").transform;
        theState = this.gameObject.GetComponent<AnimationScriptBlackTrooper>();//animation States: Idle = 0,Walk = 1, Run = 2, Shoot = 3,Punch = 4
        NavAg = GetComponent<NavMeshAgent>();

        thePunch = punchPoint.gameObject.GetComponent<KnockBackPlayer>();


        enter = new Dictionary<State, System.Action>()
        {
            {State.Idle, enterI },
            {State.Chase, enterC },
            {State.Shoot, enterS },
            {State.Strafe, enterSt },
            {State.Patrol, enterP },
            {State.Punch, enterPu },
        };
        exit = new Dictionary<State, System.Action>()
        {
            {State.Idle, exitI },
            {State.Chase, exitC },
            {State.Shoot, exitS },
            {State.Strafe, exitSt },
            {State.Patrol, exitP },
            {State.Punch, exitPu },

        };
        execute = new Dictionary<State, System.Action>()
        {
            {State.Idle, executeI },
            {State.Chase, executeC },
            {State.Shoot, executeS },
            {State.Strafe, executeSt },
            {State.Patrol, executeP },
            {State.Punch, executePu },

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
        timeToStrafe = 3;
        NavAg.speed = shootSpeed;
    }
    void exitS()
    {
    }
    void executeS()
    {
        NavAg.destination = Plyr.position;
        fireGun += Random.Range(0, 100);
        if (fireGun > whenToFire)
        {
            Fire();
            if (Random.Range(0, 3) == 0)
                fireGun = fireGun - (whenToFire / 10);
            else
            {
                fireGun = fireGun - whenToFire;
                timeToStrafe--;
            }
        }




        if (!InRangeToShoot() && CanSee())
            Transition(State.Chase);
        if (!CanSee())
            Transition(State.Idle);
        if (timeToStrafe < 1)
            Transition(State.Strafe);
        if (Vector3.Distance(transform.position, Plyr.position) < 4)
            Transition(State.Punch);
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
    void enterSt()
    {
        NavAg.speed = runSpeed;
        theState.animationState = 2;
        StrafeDestination = transform.position + new Vector3(xRandom, 0f, zRandom);
        backToShooting = 5;
        StartCoroutine(shootAgain());

    }
    void exitSt()
    {
        StopCoroutine(shootAgain());
    }
    void executeSt()
    {

        NavAg.destination = StrafeDestination;

        if (Vector3.Distance(StrafeDestination, transform.position) < 1.0f)
        {

            if (InRangeToChase())
                Transition(State.Chase);
            if (InRangeToShoot())
                Transition(State.Shoot);
            if (!InRangeToChase() && !InRangeToShoot())
                Transition(State.Idle);
            if (backToShooting < 1)
                Transition(State.Shoot);
        }


    }
    IEnumerator shootAgain()
    {
        backToShooting--;
        xRandom = Random.Range(7, 15);
        zRandom = Random.Range(7, 15);
        xNegative = Random.Range(0, 2);
        zNegative = Random.Range(0, 2);
        if (xNegative == 1)
            xRandom = -xRandom * (-1);
        if (zNegative == 1)
            zRandom = zRandom * (-1);
        yield return new WaitForSeconds(1);


    }
    //Patrol/////////////////////////////////////////////////////////////////
    void enterP()
    {
        NavAg.speed = walkSpeed;
        theState.animationState = 1;
        pTarget1 = PatrolTarget.Find("Patrol1");
        pTarget2 = PatrolTarget.Find("Patrol2");

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


        if (Vector3.Distance(transform.position, pTarget.transform.position) < 2)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PatrolTarget")
        {
            PatrolTarget = other.transform;
            pTarget = PatrolTarget.Find("Patrol1");
            onPatrol = true;
        }
    }

    //Punch/////////////////////////////////////////////////////////////////
    void enterPu()
    {
        theState.animationState = 4;
        NavAg.speed = 0f;
        thePunch.punchState = true;

    }
    void exitPu()
    {
        thePunch.punchState = false;
    }
    void executePu()
    {

        if (PunchIsPlaying())
        {
            if (InRangeToChase())
                Transition(State.Chase);
            else if (InRangeToShoot())
                Transition(State.Shoot);
            else
                Transition(State.Idle);
        }
    }

    bool PunchIsPlaying()
    {
        return theState.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
}
