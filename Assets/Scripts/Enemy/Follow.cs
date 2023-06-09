using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    
    public Transform player;
    public float followRange = 10f;
    public float moveSpeed = 10f;
    private EnemyCombat enemyCombat;

    private void Start()
    {
        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyCombat = gameObject.GetComponent<EnemyCombat>();
    }

    private void Update()
    {

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the follow range
        if (distanceToPlayer <= followRange)
        {
            // Calculate the direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            transform.forward = direction;
            Debug.Log("Moving");

            gameObject.GetComponent<EnemyCombat>().attack(player.gameObject);

        }

    }
}
