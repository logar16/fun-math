using UnityEngine;

namespace FunMath
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 100)]
        int health = 10;

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
            {
                // TODO: Fire death event
            }
        }
    }
}
