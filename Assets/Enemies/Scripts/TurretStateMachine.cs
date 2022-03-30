using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateMachine : MonoBehaviour
{
    public enum State { Idle, KillLuke}
    public State CurState;
    private Transform Plyr;
    
    
    private Vector3 direction;

    private Dictionary<State, System.Action> enter;
    private Dictionary<State, System.Action> exit;
    private Dictionary<State, System.Action> execute;

    [Tooltip("default speed for navmeshes is 3.5")]
    public float SPD;

    //Idle variables

    public Transform towerHead;
    private float leftOrRight = 0;
    private float secondsTurning;

    Vector3 IRos;

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
    public float BlasterBoltF = 5.5f;
    private int numberOfBullets;
    private float fireGun;
    public float whenToFire = 10000;

    //public float RocketF = 1.7f;
    int JmpTme = 0;

    




    // Start is called before the first frame update
    void Start()
    {
        Plyr = GameObject.FindGameObjectWithTag("Player").transform;
        IRos = towerHead.forward;
        


        
        

        enter = new Dictionary<State, System.Action>()
        {
            {State.Idle, entI },
            {State.KillLuke, entK }
            
        };
        exit = new Dictionary<State, System.Action>()
        {
            {State.Idle, extI },
            {State.KillLuke, extK }

        };
        execute = new Dictionary<State, System.Action>()
        {
            {State.Idle, excI },
            {State.KillLuke, excK }

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
        towerHead.rotation = Quaternion.LookRotation(IRos);
    }
    void extI()
    {
        
    }
    void excI()
    {
        if (secondsTurning >= 0)
        {
            if (leftOrRight == 0)
                towerHead.Rotate(new Vector3(0, .1f, 0));
            else if (leftOrRight == 1)
                towerHead.Rotate(new Vector3(0, -.1f, 0));
            secondsTurning--;
        }
        else
        {
            secondsTurning = Random.Range(500, 2500);
            leftOrRight = Mathf.Abs(leftOrRight - 1);
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
    }
    void extK()
    {
        
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

        towerHead.transform.LookAt(Plyr.position);

        // Initiate Bullet
        //if (Input.GetKeyDown(KeyCode.E))

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
            Transition(State.Idle);
        }
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
        Fired.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity;
        Fired.GetComponent<Rigidbody>().AddForce(bulletDirection * .5f, ForceMode.Impulse);
    }
    
}
