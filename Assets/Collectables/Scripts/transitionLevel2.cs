using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class transitionLevel2 : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
{
        if(other.gameObject.tag == "Player")
            SceneManager.LoadScene("Level2");
        //SceneManager.scene(4);
}
}
