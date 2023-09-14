using UnityEngine;

namespace FunMath
{
    public class EnemyMovement : MonoBehaviour
    {
        public EnemyController controller;

        private Transform player;

        private float moveSpeed = 25f;

        private float stoppingDistance = 2f;

        private Vector3 direction;

        private Arrow arrow;

        private void Start()
        {
            player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
            if (player == null)
            {
                Debug.Log("Player is not available");
            }
            arrow = FindObjectOfType<Arrow>();
        }
        // Update is called once per frame       
        void Update()
        {
            // Setting the direction of the enemyMovement to be towards the player character.         
            direction = player.position - transform.position;
            CheckForCollisions();
        }

        void FixedUpdate()
        {
            // Move our character
            
            Vector3 pos = player.position;
            controller.Move(moveSpeed * Time.fixedDeltaTime, stoppingDistance, direction.normalized, pos);
        }

        private void CheckForCollisions()
        {
            // Define a small radius around the enemy to check for collisions
            float checkRadius = 0.1f;

            // Get all colliders in the check area
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius);

            foreach (Collider2D collider in colliders)
            {
                // Check if the collider belongs to another enemy (you can use tags or layers for this)
                if (collider.CompareTag("Enemy") && collider != this.gameObject.GetComponent<Collider2D>())
                {
                    // Calculate a direction away from the other enemy
                    Vector2 awayFromOtherEnemy = (transform.position - collider.transform.position).normalized;

                    // Apply a force to move away from the other enemy
                    controller.MoveAway(awayFromOtherEnemy);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision != null)
            {
                if (arrow != null)
                {
                    controller.ReceiveAttack(arrow.Operator, arrow.Modifier);
                }
            }
        }
    }
}
