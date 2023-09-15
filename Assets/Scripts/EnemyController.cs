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
        private Vector3 velocity = Vector3.zero;
        private Animator anim;
        private SpriteRenderer spriteRenderer;
        private HealthCalculator health;

        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        public Color InversedColor;
        public TMPro.TextMeshProUGUI text;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            health = gameObject.GetComponent<HealthCalculator>();
            anim = GetComponent<Animator>();
            anim.SetBool("IsRunning", true);

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

            // If health is negative, show inversed colors
            if(health.IsNegativeHealth())
            {
                // Tint is inversed
                spriteRenderer.color = InversedColor;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
            string textStr = "<color=\"orange\">" + health.GetHealth().ToString();
            text.text = textStr;
            text.transform.rotation = Quaternion.identity;
        }

        public void Move(float move, float stoppingDistance, Vector3 direction, Vector3 playerPosition)
        {
            //only control the enemy if grounded or airControl is turned on
            if (grounded)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 5f, rigidBody.velocity.y) * direction;
                // And then smoothing it out and applying it to the character
                rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);

                if (Vector2.Distance(transform.position, playerPosition) <= stoppingDistance)
                {
                    rigidBody.velocity = Vector3.zero;
                    anim.SetBool("IsAttacking", true);
                }
                else
                {
                    anim.SetBool("IsAttacking", false);
                }

                // If the enemy is moving left and the enemy is facing right...
                if (rigidBody.velocity.x < 0)
                {
                    // ... flip the enemy.
                    spriteRenderer.flipX = true;
                }
                // Otherwise if the enemy is moving the right and the enemy is facing left...
                else if (rigidBody.velocity.x > 0)
                {
                    // ... flip the enemy.
                    spriteRenderer.flipX = false;
                }
            }
        }

        public void MoveAway(Vector2 distance)
        {
            rigidBody.AddForce(distance * 2.0f, ForceMode2D.Impulse);
        }
    }
}