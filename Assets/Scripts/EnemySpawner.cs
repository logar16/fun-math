using System.Collections;
using UnityEngine;

namespace FunMath
{
    public class EnemySpawner : MonoBehaviour
    {
        private GameObject enemyPrefab; // Reference to the enemy prefab
        [SerializeField]
        public int maxEnemies = 5; // Maximum number of enemies to spawn
        private int numSpawnEnemies = 0;
        [SerializeField]
        private float spawnDelay = 5.0f; // Delay between enemy spawns (in seconds)
        private Vector2 spawnPosition = Vector2.zero;
        private string prefabPath = "Characters/Enemy/Enemy_Boar";


        private void Start()
        {
            enemyPrefab = Resources.Load<GameObject>(prefabPath);
            if(enemyPrefab == null )
            {
                Debug.Log("enemy is unavailable");
            }
            spawnPosition = transform.position;

            // Start spawning enemies with a delay
            StartCoroutine(SpawnEnemyWithDelay());
        }

        private void SpawnEnemy()
        {
            // Create a new enemy instance from the prefab
            if (enemyPrefab != null)
            {
                // Randomly select a spawn point
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
