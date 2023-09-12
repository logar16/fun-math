using UnityEngine;

namespace FunMath
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 100)]
        int health = 10;

        public void ModifyHealth(int change)
        {
            health += change;
            if (health < 0)
            {
                // TODO: Fire death event
            }
        }
    }
}
