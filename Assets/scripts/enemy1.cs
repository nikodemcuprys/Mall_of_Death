using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy1 : MonoBehaviour
{
   public float speed;
   public float stopingDistance;
   public float retreatdistance;
   public Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position)> stopingDistance){
            transform.position = Vector2.MoveTowards(transform.position, player.position,speed*Time.deltaTime);
        } else if(Vector2.Distance(transform.position, player.position)< stopingDistance && Vector2.Distance(transform.position, player.position) > retreatdistance){
            transform.position = this.transform.position;
        } else if(Vector2.Distance(transform.position, player.position)< retreatdistance){
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed*Time.deltaTime);}
    }
}
