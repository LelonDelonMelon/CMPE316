using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform player;
    public float movementSpeed = 5f;
    private EnemyCombat enemyCombat;
    public float stoppingDistance = 1f;
    private bool isFollowing = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyCombat = GetComponent<EnemyCombat>();
    }

    private void FixedUpdate()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
       // Debug.Log("Is following: " + isFollowing);

        // If the enemy is outside the stopping distance, move towards the player
        if (distanceToPlayer > stoppingDistance)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position += direction * movementSpeed * Time.fixedDeltaTime;
            isFollowing = true;

        }

        // Look at the player
        transform.LookAt(player);

        // Attack the player
        enemyCombat.attack(player.gameObject);
    }
    public void setIsFollowing(bool b)
    {
        isFollowing = b;
    }
}
