using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineTurret : MonoBehaviour
{
    
    //States
    public enum State { Idle, Shoot}
    public State CurState;
    private Dictionary<State, System.Action> enter;
    private Dictionary<State, System.Action> exit;
    private Dictionary<State, System.Action> execute;

    //Player
    private Transform Plyr;
    public LayerMask player;

    //Entity
    [Tooltip("Entity Stats")]
    private float VisRng = 110;
    private float VisDist = 50;

    //Bullets and Guns
    public GameObject Head;
    public Transform gunTip1;
    public Transform gunTip2;
    GameObject bulletsFired;
    public GameObject bulletType;
    private float fireGun;
    public float whenToFire = 5000;


    //Sounds
    public AudioSource sounds;
    public AudioClip pew;


    // Start is called before the first frame update
    void Start()
    {
        //initialization
        Plyr = GameObject.FindGameObjectWithTag("Player").transform;
        enter = new Dictionary<State, System.Action>()
        {
            {State.Idle, enterI },
            {State.Shoot, enterS }
        };
        exit = new Dictionary<State, System.Action>()
        {
            {State.Idle, exitI },
            {State.Shoot, exitS }

        };
        execute = new Dictionary<State, System.Action>()
        {
            {State.Idle, executeI },
            {State.Shoot, executeS }

        };

        CurState = State.Idle;
    }

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
    private bool InRangeToShoot()
    {
        return Vector3.Distance(transform.position, Plyr.position) < VisDist;
    }
    //Idle////////////////////////////////////////////////////////////////////////////////////////////
    void enterI()
    {

    }
    void exitI()
    {
    }
    void executeI()
    {
        if (InRangeToShoot())
            Transition(State.Shoot);
    }

    //Shoot////////////////////////////////////////////////////////////////////////////////////////////
    void enterS()
    {
        fireGun = whenToFire;
    }
    void exitS()
    {
    }
    void executeS()
    {
        Head.transform.LookAt(Plyr);
        fireGun += Random.Range(0, 100);
        if (fireGun > whenToFire)
            {
            Fire1();
            Fire2();
            if (Random.Range(0, 3) == 0)
                fireGun = fireGun - (whenToFire / 10);
            else
                fireGun = fireGun - whenToFire;
        }
        

        if (!InRangeToShoot())
            Transition(State.Idle);
    }
    void Fire1()
    {
        //sounds.PlayOneShot(pew);
        bulletsFired = Instantiate(bulletType, gunTip1.position, Quaternion.identity);

        bulletsFired.GetComponent<Rigidbody>().mass = .2f;

        Vector3 bulletDirection = Plyr.position - bulletsFired.transform.position;
        bulletsFired.transform.forward = bulletDirection.normalized;

        bulletsFired.GetComponent<Rigidbody>().useGravity = false;
        bulletsFired.GetComponent<Rigidbody>().angularDrag = 0;

        bulletsFired.GetComponent<Rigidbody>().AddForce(bulletDirection * 1f, ForceMode.Impulse);
    }

    void Fire2()
    {
        //sounds.PlayOneShot(pew);
        bulletsFired = Instantiate(bulletType, gunTip2.position, Quaternion.identity);

        bulletsFired.GetComponent<Rigidbody>().mass = .2f;

        Vector3 bulletDirection = Plyr.position - bulletsFired.transform.position;
        bulletsFired.transform.forward = bulletDirection.normalized;

        bulletsFired.GetComponent<Rigidbody>().useGravity = false;
        bulletsFired.GetComponent<Rigidbody>().angularDrag = 0;

        bulletsFired.GetComponent<Rigidbody>().AddForce(bulletDirection * 1f, ForceMode.Impulse);
    }

}
