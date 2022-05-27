using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    // Objects
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemy;

    // Stats
    public float speed;
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity = false;

    public int explosionDamage = 10;
    public float explosionRange;

    public int maxCollisions = 1;
    public float maxLifetime;
    public bool explodeOnTouch = false;

    int collisions;
    PhysicMaterial physicsMat;

    // -============================================================-

    private void Start() {
        Setup();
    }

    private void Update() {
        if (collisions > maxCollisions) Explode();


        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    // -============================================================-

    void Setup() {

        physicsMat = new PhysicMaterial();
        physicsMat.bounciness = bounciness;
        physicsMat.frictionCombine = PhysicMaterialCombine.Minimum;
        physicsMat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physicsMat;

        rb.useGravity = useGravity;
    }

    void Explode() {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<enemy1>().TakeDamage(explosionDamage);
        }
        Invoke("Delay",0.05f);
    }
    void Delay(){
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Bullet")) return;

        collisions++;

        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

}
