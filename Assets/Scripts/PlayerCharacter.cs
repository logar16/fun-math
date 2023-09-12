using UnityEngine;

namespace FunMath
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField]
        [Range(0.1f, 2f)]
        float speed = 1f;

        protected Rigidbody2D rb;

        bool Jumping = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Jumping)
            {
                return;
            }
            // Get WASD inputs to move circle left and right
            float horizontalInput = Input.GetAxis("Horizontal");

            // Apply force to the rigidbody in the desired direction
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
            //if movement is changing direction, significantly reduce the velocity
            if (movement.x * rb.velocity.x < 0)
            {
                rb.AddForce(movement * speed * 5f, ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(movement * speed, ForceMode2D.Force);
            }

            //if (Mathf.Abs(rb.velocity.x) > MaxSpeed)
            //{
            //    var speed = rb.velocity.x > 0 ? MaxSpeed : -MaxSpeed;
            //    rb.velocity = new Vector2(speed, rb.velocity.y);
            //}


            //Jump (but only if on the ground)
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
                Jumping = true;
                // Set a timer to reset the Jumping flag
                Invoke("ResetJumping", 1f);
            }
        }

        void ResetJumping()
        {
            Jumping = false;
        }
    }
}
