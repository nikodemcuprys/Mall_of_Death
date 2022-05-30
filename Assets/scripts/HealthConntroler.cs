using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConntroler : MonoBehaviour
{
    public int health = 100;

    public HealthBar healthBar;

    void Start(){
        if (healthBar){
             healthBar.SetMaxHealth(health);
        } else {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
