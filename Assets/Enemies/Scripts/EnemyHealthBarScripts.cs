using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealthBarScripts : MonoBehaviour
{

    [SerializeField] public GameObject playerRef;
    private CameraController camControl;

    [Header("Health")]
    [Tooltip("this is the max health it can have")]
    public float MaxH;
    [Tooltip("this is its current health")]
    private float CurH;

    [Header("Options")]
    [Tooltip("this is checked if the healthbar is allways visible, even at full health")]
    public bool isVisbl;

    [Header("References")]
    [Tooltip("this is a reference to the slider UI object that is the healthbar")]
    public Slider HBSl;
    [Tooltip("this is a reference to the canvas object that holds the healthbar")]
    public GameObject CanObj;
    [Tooltip("this is a reference to what the healthbar should face towards")]
    private Transform FaceT;
    [Tooltip("this is a reference to the empty object that holds the canvas")]
    public Transform HldrT;

    bool HBoff = true;//the health bar is off untill the object takes damage

    // Start is called before the first frame update
    void Start()
    {
        camControl = playerRef.GetComponent<CameraController>();

        CurH = MaxH;
        CanObj.SetActive(false);
        InvokeRepeating("HealthRegen", 0.0f, 0.25f);

        FaceT = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVisbl)
        {
            HldrT.rotation = Quaternion.Euler(0, 0, 0);//this keeps the healthbar above the object at all times
            if (CurH < MaxH && HBoff)
            {
                CanObj.SetActive(true);
                HBoff = false;
            }
            if (!HBoff)
            {
                CanObj.transform.rotation = Quaternion.LookRotation(FaceT.position - CanObj.transform.position);
            }
        }
        else { CanObj.SetActive(true); }
    }

    public void TakeDamage(float dmg)
    {

        CurH -= dmg;
        if (CurH > MaxH) { CurH = MaxH; }//healing will inevitibly come up, and healing is just -damage. don't want to allow over healing (for now, may be something i try out later)
        if (CurH < 0) { CurH = 0; }      //this ensures that if you damage an enemy past the amount of health it has left, the healthbar dosen't fill backwards
        HBSl.value = CurH / MaxH;            //this moves the slider based off a percentage of the remaining health out of the max health

        if (CurH <= 0)
        {
            //camControl.ToggleLockOn(!camControl.LockedOn);
            Destroy(this.gameObject);
            Destroy(this.GetComponent<EnemyHealthBarScripts>());
            Destroy(CanObj);
            Destroy(this);
        }
    }

    public void HealthRegen()
    {
        TakeDamage(-1);
    }
}
