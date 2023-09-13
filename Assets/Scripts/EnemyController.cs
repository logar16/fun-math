using UnityEngine;
using UnityEngine.Events;

namespace FunMath
{
    public class EnemyController : MonoBehaviour
    {
        [Range(0, .3f)][SerializeField] private float movementSmoothing = .05f;   // How much to smooth out the movement
        [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private Transform groundCheck;                           // A position marking where to check if the enemy is grounded.

        public GameObject deathEffect;

        const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded;            // Whether or not the enemy is grounded.
        private Rigidbody2D rigidBody;
        private bool facingLeft = true;  // For determining which way the enemy is currently facing.
        private Vector3 velocity = Vector3.zero;

        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        public void Move(float move, float stoppingDistance, Vector3 playerPosition)
        {
            //only control the enemy if grounded or airControl is turned on
            if (grounded)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 2f, rigidBody.velocity.y);
                // And then smoothing it out and applying it to the character
                rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);       

                if (Vector2.Distance(transform.position, playerPosition) <= stoppingDistance)
                {
                    rigidBody.velocity = Vector3.zero;
                }

                // If the enemy is moving left and the enemy is facing right...
                if (move > 0 && facingLeft)
                {
                    // ... flip the enemy.
                    Flip();
                }
                // Otherwise if the enemy is moving the right and the enemy is facing left...
                else if (move < 0 && !facingLeft)
                {
                    // ... flip the enemy.
                    Flip();
                }
            }
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();
        }

        private void FixedUpdate()
        {
            bool wasGrounded = grounded;
            grounded = false;

            // The enemy is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
        }

        private void Flip()
        {
            // Switch the way the enemy is labelled as facing.
            facingLeft = !facingLeft;

            // Multiply the enemy's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void Die()
        {
            
            Destroy(gameObject);
        }
    }
}
