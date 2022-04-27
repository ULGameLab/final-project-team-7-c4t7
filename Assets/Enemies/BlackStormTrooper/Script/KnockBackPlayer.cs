using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackPlayer : MonoBehaviour
{
    public bool punchState = false;
    public GameObject player;
    private Vector3 playerDirection;
    public bool punchHitting = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.AddComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(punchHitting)
            knockBack();
    }
    void knockBack()
    {

        if (punchState)
        {
            StartCoroutine(moveBackWards());
            Debug.Log("Hit");
        }
        else
        {
            StopCoroutine(moveBackWards());
        }
    }
    IEnumerator moveBackWards()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<Rigidbody>().AddForce(player.transform.up * Time.deltaTime, ForceMode.VelocityChange);
        player.GetComponent<CharacterController>().enabled = true;
        yield return new WaitForSeconds(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            punchHitting = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            punchHitting = false;
    }
}
