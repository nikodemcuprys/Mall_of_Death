using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemy1 : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform attackPoint;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject bullet;

   // partoling
    public Vector3 walkPoint;
    bool walkPointSet;
   // attack
    public float walkPointRange;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
   // states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Update() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Partoling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Awake() {
        player = GameObject.Find("MainHero").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Partoling(){
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        // when reached walkpoint
        if (distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }
    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }
    private void AttackPlayer(){
        agent.SetDestination(player.position);
        transform.LookAt(player);

        if(!alreadyAttacked){

            Rigidbody rb = Instantiate(bullet,attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAtttack),timeBetweenAttacks);
        }
    }
    private void ResetAtttack(){
        alreadyAttacked = false;
    }
}
