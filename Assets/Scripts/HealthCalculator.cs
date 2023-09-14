using System;
using UnityEngine;

namespace FunMath
{
    public class HealthCalculator : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 100)]
        int health = 10;

        public void ModifyHealth(OperationType operation, int modifier)
        {
            switch (operation)
            {
                case OperationType.Addition:
                    health += modifier;
                    break;
                case OperationType.Subtraction:
                    health -= modifier;
                    break;
                case OperationType.Multiply:
                    health *= modifier;
                    break;
                case OperationType.Divide:
                    health = (int)Math.Round((float)modifier / health, 0, MidpointRounding.AwayFromZero);
                    break;
            }

            Debug.Log($"{gameObject} health :{health}");
        }

        private void Update()
        {
            if (health == 0)
            {
                // Note: This script doesn't distinguish between player and enemy contexts. Please ensure both player/enemy
                // prefabs have an attached animator and include the praramter 'IsDead' to transition into the dying animation.
                Animator animator = gameObject.GetComponent<Animator>();
                animator.SetBool("IsDead", true);

                // TODO: move destroy as an animation trigger
                // Destroy(gameObject);
            }
        }
    }
}
