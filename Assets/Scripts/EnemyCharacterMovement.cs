using UnityEngine;

namespace FunMath
{
    public class EnemyCharacterMovement : MonoBehaviour
    {
        public EnemyController controller;

        public GameObject player;

        public float moveSpeed = 40f;

        public float stoppingDistance = 2f;

        float horizontalMove = 0f;

        // Update is called once per frame
        void Update()
        {
            // Setting to -1 to tell the enemy to move towards the left direction by default.
            horizontalMove = -1 * moveSpeed;

        }

        void FixedUpdate()
        {
            // Move our character
            Vector3 pos = player.transform.position;
            controller.Move(horizontalMove * Time.fixedDeltaTime, stoppingDistance, pos);
        }
    }
}
