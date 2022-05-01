using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class transitionWin : MonoBehaviour
{
      
   void OnTriggerEnter(Collider other)
{
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(1);
            //SceneManager.scene(1);
        }

}
}
