using UnityEngine;

namespace FunMath
{
    public class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 100)]
        int Health = 10;

        [SerializeField]
        [Range(0.01f, 10f)]
        float Size = 1f;

        [SerializeField]
        [Range(1f, 10f)]
        float MaxSpeed = 5f;

        protected Rigidbody2D rb;
        
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Mathf.Abs(rb.velocity.x) > MaxSpeed)
            {
                var speed = rb.velocity.x > 0 ? MaxSpeed : -MaxSpeed;
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }

        protected void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                // TODO: Die
            }
        }

        protected void ModifySize(float modifier)
        {
            Size *= modifier;
            if (Size <= 0) 
            {
                // TODO: Kill the character?
            }
        }
    }
}
