using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    private PlayerInputs inputs;
    public AudioSource sounds;
    public AudioClip swish;
    public GameObject BaseLightsaber;
    //float Damage;
    public GameObject StrikeZone;
    bool AttackReady;
    public GameObject DeflectZone;
    bool DeflectReady;
    public float DeflectWait;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<PlayerInputs>();
        AttackReady = true;
        DeflectReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackReady && inputs.Attack.PressedDown())
        {
            sounds.PlayOneShot(swish);
            StartCoroutine(Attack());
            StartCoroutine(AttackReadying());
        }
        /*
        if (DeflectReady && Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(Deflect());
            StartCoroutine(DeflectReadying());
        }
        */
        if (DeflectReady && inputs.Block.PressedDown())
        {
            //DeflectReady = false;
            BaseLightsaber.SetActive(false);
            DeflectZone.SetActive(true);
            AttackReady = false;
        }
        if (inputs.Block.PressedUp())
        {
            DeflectZone.SetActive(false);
           // BaseLightsaber.SetActive(true);
            AttackReady = true;
        }

    }


    IEnumerator Attack()
    {
        DeflectReady = false;
        AttackReady = false;
        BaseLightsaber.SetActive(false);
        StrikeZone.SetActive(true);
        //RepulseTimer = 0f;
        yield return new WaitForSeconds(.05f);
        StrikeZone.SetActive(false);
     //   BaseLightsaber.SetActive(true);
    }

    IEnumerator AttackReadying()
    {
        yield return new WaitForSeconds(DeflectWait);
        DeflectReady = true;
        AttackReady = true;

    }

    public float GetWait()
    {
        return DeflectWait / 100;
    }

}
