using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class transitionLevel2 : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
{
        SceneManager.LoadScene(4);
        //SceneManager.scene(4);
}
}
