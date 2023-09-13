using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class EnemyCharacter : MonoBehaviour
    {
        public int health = 100;

        void Start()
        {
        
        }

        void Update()
        {
        
        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            // Enemy will not die if health drop below 0
            if (health == 0)
            {
                Die();
            }
        }

        void Die()
        {
            
        }
    }
}
