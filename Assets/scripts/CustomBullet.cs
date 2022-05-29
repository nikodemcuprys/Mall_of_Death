using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    // Objects
    public Rigidbody rb;
    public int damage = 1000000;


    void OnTriggerEnter (Collider hitInfo) {
        HealthConntroler player = hitInfo.GetComponent<HealthConntroler>();
        if (player != null){
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
