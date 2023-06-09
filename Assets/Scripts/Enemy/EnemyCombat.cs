using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 40;
    [SerializeField] private int currentHealth;

    [SerializeField] private GameObject enemySpawner;
    private EnemySpawner es;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        enemySpawner = GameObject.Find("EnemySpawner");
        es = enemySpawner.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        if(health - damage > 0)
        {
            currentHealth = health - damage;
            Debug.Log("Took damage" + " " +  damage);
        }
        else
        {
            Debug.Log("Ded");
            Die();
        }
        if(currentHealth <= 20)
        {
            Debug.Log("here");
            changeLayer();
            gameObject.transform.GetComponent<Renderer>().material.color = Color.blue;
        }
        health = currentHealth;
    }

    public void attack(GameObject target)
    {
         target.GetComponent<PlayerCombat>().takeDamage(this.damage);
    }
    public void Die()
    {
        // Destroy(gameObject);
        this.gameObject.SetActive(false);
        int numOfEnemies = es.getCurrentNumberOfEnemies();
        es.setCurrentNumberOfEnemies(numOfEnemies - 1);
    }
    public void Reset()
    {
        health = 100;
        currentHealth = health;
    }

    public void changeLayer()
    {

        gameObject.layer = LayerMask.NameToLayer("Pickable");

    }
}
