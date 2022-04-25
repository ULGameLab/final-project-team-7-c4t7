using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBullet : MonoBehaviour
{
    GameObject Bullet;
    float lifeSpan = 5;//this is fixed as no real situation should take bullet travel time over 5 seconds.
    public float Damage = 5;
    float DMG;//this is the damage that the bullet will do, calculated off current speed, mass, and damage modifyers from the gun.


    void Start()
    {
        Bullet = this.gameObject;
        StartCoroutine(KillBullet(lifeSpan));
    }

    IEnumerator KillBullet(float temp)
    {
        
        yield return new WaitForSeconds(temp);
        Destroy(Bullet);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DMG = (Damage);//scale damage based off of the bullet's speed.
        if (collision.collider.tag == "Player")
        {
            HeathBar HB = collision.gameObject.GetComponent<HeathBar>();
            if (HB != null) { HB.TakeDamage(DMG); }
        }

        if (collision.collider.tag == "Enemy")
        {
            EnemyHealthBarScripts HBE = collision.gameObject.GetComponent<EnemyHealthBarScripts>();
            if (HBE != null) { HBE.TakeDamage(DMG); }
        }
        Destroy(Bullet);
    }
}
