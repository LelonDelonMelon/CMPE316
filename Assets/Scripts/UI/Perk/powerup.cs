using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    public float multiplier=1.4f;
    public GameObject pickupEffect;
   void OnTriggerEnter(Collider other){

    if(other.CompareTag("Player")){
        Pickup(other);
    }

    void Pickup(Collider player){

        Instantiate(pickupEffect, transform.position, transform.rotation );
         
         playerstat stats=player.GetComponent<playerstat>();
         stats.health*=multiplier;
         stats.damage*=multiplier;
         player.transform.localScale*=multiplier;
        
        Destroy(gameObject);
    }

   }
}
