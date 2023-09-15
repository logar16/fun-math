using UnityEngine;

namespace FunMath
{
    public class Arrow : MonoBehaviour
    {
        public float speed = 20f;
        public Rigidbody2D arrowRigidBody;

        public OperationType Operator;
        public int Modifier = 1;

        [SerializeField]
        private AudioClip launchSound;
        
        [SerializeField]
        private AudioClip hitSound;

        // Start is called before the first frame update
        void Start()
        {
            // Fly forward
            arrowRigidBody.velocity = transform.right * speed;
            FindObjectOfType<AudioManager>().PlaySound(launchSound);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.name);
            HealthCalculator enemyHealth = collision.GetComponent<HealthCalculator>();
            if (enemyHealth != null)
            {
                FindObjectOfType<AudioManager>().PlaySound(hitSound);
                enemyHealth.ModifyHealth(Operator, Modifier);
                Destroy(gameObject);
            }
        }
    }
}
