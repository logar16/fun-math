using System.Collections;
using UnityEngine;

namespace FunMath
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab; // Reference to the enemy prefab
        public int maxEnemies = 25; // Maximum number of enemies to spawn
        private int numSpawnEnemies = 0;
        private Transform player;
        private float spawnDelay = 0.0f; // Delay between enemy spawns (in seconds)
        private Vector2 spawnPosition = Vector2.zero;
        private float offset = 5.0f;
        private float closeRange = 10.0f;


        private void Start()
        {
            // Initialize the array to store spawn positions
            player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
            if(enemyPrefab == null )
            {
                Debug.Log("enemy is unavailable");
            }
            spawnDelay = 3.0f;
            spawnPosition = transform.position;

            // Start spawning enemies with a delay
            StartCoroutine(SpawnEnemyWithDelay());
        }

        private void Update()
        {    
            if (Vector2.Distance(player.transform.position, transform.position) <= closeRange)
            {
                spawnPosition = new Vector2(transform.position.x - offset, transform.position.y);
            }
        }

        private void SpawnEnemy()
        {
            // Create a new enemy instance from the prefab
            if (enemyPrefab != null)
            {
                // Randomly select a spawn point
                Debug.Log("spawn position " + spawnPosition);
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                numSpawnEnemies += 1;               
            }
        }

        private IEnumerator SpawnEnemyWithDelay()
        {
            while (numSpawnEnemies <= maxEnemies)
            {
                // Wait for the specified delay before spawning the next enemy
                yield return new WaitForSeconds(spawnDelay);
                SpawnEnemy();
            }
        }

    }
}
