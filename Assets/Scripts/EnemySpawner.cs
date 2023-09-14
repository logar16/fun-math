using System.Collections;
using UnityEngine;

namespace FunMath
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab; // Reference to the enemy prefab
        public int maxEnemies = 25; // Maximum number of enemies to spawn
        private int numSpawnEnemies = 0;
        private float spacing = 10.0f;
        private int numSpawnPoints = 2;
        private Transform player;
        private float spawnDelay = 0.0f; // Delay between enemy spawns (in seconds)
        private Vector2 spawnPosition = Vector2.zero;


        private void Start()
        {
            // Initialize the array to store spawn positions
            player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
            spawnDelay = 5.0f;
            if (player == null)
            {
                Debug.Log("Player is not available");
            }

            // Calculate spawn positions based on the player's position
            CalculateSpawnPosition();

            // Start spawning enemies with a delay
            StartCoroutine(SpawnEnemyWithDelay());
        }

        private void Update()
        {           
            
        }

        private void CalculateSpawnPosition()
        {
            // Calculate the starting position based on the player's position
            Vector2 startPosition = player.position + Vector3.right * (-spacing * (numSpawnPoints / 2));
          
            // Ensure no enemy is spawned from the (0,0,0) vector
            spawnPosition = startPosition + Vector2.right * spacing;
            
            if (spawnPosition == Vector2.zero)
            {
                spawnPosition.x += spacing; // Offset the spawn point if it's at (0,0,0)
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
            if (numSpawnEnemies <= maxEnemies)
            {
                while (true)
                {
                    // Wait for the specified delay before spawning the next enemy
                    yield return new WaitForSeconds(spawnDelay);
                    SpawnEnemy();
                }
            }
                      
        }

    }
}
