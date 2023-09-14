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
                Destroy(gameObject);
            }
            OnHealthChangeData data = new OnHealthChangeData();
            data.Operation = operation;
            data.Modifier = modifier;
            data.PrevHealth = prevHealth;
            data.ResultantHealth = health;
            data.modifiedGameObject = gameObject;
            OnHealthChanged?.Invoke(data);
        }


    }
}
