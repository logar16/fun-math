using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class Projectile : MonoBehaviour
    {
        public OperationType Operator;
        public int Modifier = 1;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var health = collision.gameObject.GetComponent<HealthCalculator>();
            if (health)
            {
                health.ModifyHealth(Operator, Modifier);
            }
        }
    }
}
