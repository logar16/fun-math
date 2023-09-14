using System;
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

        [SerializeField][Range(0, 100)] private int health = 100;
        private readonly int maxHealth = 100;

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

            if (health > maxHealth)
            {
                health = maxHealth; 
            }

            CheckHealth();

            OnHealthChangeData data = new OnHealthChangeData();
            data.Operation = operation;
            data.Modifier = modifier;
            data.PrevHealth = prevHealth;
            data.ResultantHealth = health;
            data.modifiedGameObject = gameObject;
            OnHealthChanged?.Invoke(data);
        }

        private void CheckHealth()
        {
            Debug.Log($"{gameObject.name} health: {health}");

            bool isPlayer = gameObject.name == "Player";
            bool isPlayerDead = isPlayer && health <= 0;
            bool isEnemyDead = !isPlayer && health == 0;

            if (isPlayer)
            {
                PlayerController player = gameObject.GetComponent<PlayerController>();
                player.HealthBar.UpdateHealthBar(health, maxHealth);
            }

            if (isPlayerDead || isEnemyDead)
            {
                CharacterDie();
            }
        }

        private void CharacterDie()
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
