using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 40;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject perkSprite;


    

    [SerializeField] private GameObject enemySpawner;
    private EnemySpawner es;

    void Start()
    {
        currentHealth = health;
        enemySpawner = GameObject.Find("EnemySpawner");
        es = enemySpawner.GetComponent<EnemySpawner>();
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
        int randomChance = Random.Range(1, 10);
        int randomChance2 = Random.Range(1, 10);
        // Destroy(gameObject);
        this.gameObject.SetActive(false);
        int numOfEnemies = es.getCurrentNumberOfEnemies();
        es.setCurrentNumberOfEnemies(numOfEnemies - 1);

        if(randomChance % randomChance2 == 0)
        {
            dropPerk();
        }
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
    public void dropPerk()
    {
        Instantiate(perkSprite, transform.position, Quaternion.identity);

    }
}
