using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 100;
    public float spawnInterval = 1f;

    private List<GameObject> enemyPool;
    private float spawnTimer;
    [SerializeField] private int currentNumberOfEnemies = 0;

    private void Start()
    {
        enemyPool = new List<GameObject>();
        spawnTimer = spawnInterval;

        // Pre-populate the object pool
        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    private void Update()
    {
        //spawnTimer -= Time.deltaTime;

        if (currentNumberOfEnemies < maxEnemies)
        {
            StartCoroutine(spawner());
            //SpawnEnemy();
          //  spawnTimer = spawnInterval;
        }
    }
    IEnumerator spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        // Search for an inactive enemy in the pool
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                currentNumberOfEnemies++;

                if (LayerMask.LayerToName(enemyPool[i].layer) == "Pickable")
                {
                    enemyPool[i].layer = LayerMask.NameToLayer("Enemy");
                    enemyPool[i].GetComponent<Renderer>().material.color = Color.red;
                    enemyPool[i].GetComponent<EnemyCombat>().Reset();
                }
                // Activate the enemy and position it
                Vector3 randomPosition = GetRandomPosition();

                enemyPool[i].SetActive(true);
                enemyPool[i].transform.position = randomPosition;
                return;
            }
        }

        // If no inactive enemy is found and the maximum limit hasn't been reached, instantiate a new one and add it to the pool
        if (currentNumberOfEnemies < maxEnemies)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform);
            newEnemy.SetActive(true);
            newEnemy.transform.position = transform.position;
            enemyPool.Add(newEnemy);
            currentNumberOfEnemies++;
        }
    }
    public int getCurrentNumberOfEnemies()
    {
        return currentNumberOfEnemies;
    }
    public void setCurrentNumberOfEnemies(int newNumber)
    {
        currentNumberOfEnemies = newNumber;
    }

    private Vector3 GetRandomPosition()
    {
        // Modify these values according to your desired range
        float minX = -10f;
        float maxX = 10f;
        float minY = 0f;
        float maxY = 5f;
        float minZ = -10f;
        float maxZ = 10f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        //Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        //Vector3 randomPosition = new Vector3(509.096436f, 17.6499996f, 600.366882f);

        return transform.position;
    }
}
