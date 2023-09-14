using UnityEngine;

namespace FunMath
{
    public class EnemyMovement : MonoBehaviour
    {
        public EnemyController controller;

        public GameObject player;

        public float moveSpeed = 50f;

        public float stoppingDistance = 0.5f;

        float horizontalMove = 0f;

        // Update is called once per frame
        void Update()
        {
            // Setting the direction of the enemyMovement to be towards the player character.
            horizontalMove = 0 * moveSpeed;

        }

        void FixedUpdate()
        {
            // Move our character
            Vector3 pos = player.transform.position;
            controller.Move(horizontalMove * Time.fixedDeltaTime, stoppingDistance, pos);
        }
    }
}
