using System;
using System.Collections;
using UnityEngine;

namespace FunMath
{
    public struct OnHealthChangeData
    {
        public OperationType Operation;
        public int Modifier;
        public int ResultantHealth;
        public int PrevHealth;
        public GameObject modifiedGameObject;
    }

    public class HealthCalculator : MonoBehaviour
    {
        // Event for when the inventory changes
        public delegate void HealthChanged(OnHealthChangeData onHealthChangeData);
        public event HealthChanged OnHealthChanged;

        [SerializeField]
        [Range(0, 100)]
        int health = 10;

        public void ModifyHealth(OperationType operation, int modifier)
        {
            int prevHealth = health;
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
                    health = (int)Math.Round(health / (float)modifier, 0, MidpointRounding.AwayFromZero);
                    break;
            }

            Debug.Log($"{gameObject} health :{health}");
            if (health == 0)
            {
                // Start the coroutine to handle the delay before destroying the GameObject
                StartCoroutine(DestroyAfterDelay(2.0f));

                // Note: This script doesn't distinguish between player and enemy contexts. Please ensure both player/enemy
                // prefabs have an attached animator and include the praramter 'IsDead' to transition into the dying animation.
                Animator animator = gameObject.GetComponent<Animator>();
                animator.SetBool("IsDead", true);

                // TODO: move destroy as an animation trigger
                // Destroy(gameObject);
            }

            OnHealthChangeData data = new OnHealthChangeData();
            data.Operation = operation;
            data.Modifier = modifier;
            data.PrevHealth = prevHealth;
            data.ResultantHealth = health;
            data.modifiedGameObject = gameObject;
            OnHealthChanged?.Invoke(data);
        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            // Destroy the GameObject after the delay
            Destroy(gameObject);
        }
    }
}
