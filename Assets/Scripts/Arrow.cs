using UnityEngine;

namespace FunMath
{
    public class Arrow : MonoBehaviour
    {
        public float speed = 30f;
        public Rigidbody2D arrowRigidBody;

        public OperationType Operator;
        public int Modifier = 1;

        [SerializeField]
        private AudioClip launchSound;
        
        [SerializeField]
        private AudioClip hitSound;
        public TMPro.TextMeshProUGUI text;
        // Start is called before the first frame update
        void Start()
        {
            // Fly forward
            arrowRigidBody.velocity = transform.right * speed;
        
            FindObjectOfType<AudioManager>().PlaySound(launchSound);        }

        public void SetArrowData(OperationType oper, int Modifier)
        {
            this.Operator = oper;
            this.Modifier = Modifier;

            string str = "<color=\"green\">" + StringHelper.GetOperatorString(Operator) + "</color>";
            str += "<color=\"yellow\">" + Modifier + "</color>";

            text.text = str;

        }

        private void Update()
        {
            text.transform.rotation = Quaternion.identity;
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
