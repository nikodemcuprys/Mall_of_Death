using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConntroler : MonoBehaviour
{
    public float health = 100.0f;

    public void TakeDamage(int damage){
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
