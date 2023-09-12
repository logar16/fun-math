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
        
        void Start()
        {
        
        }

        void Update()
        {
        
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
