using System;
using UnityEngine;

namespace FunMath
{
    public class Health : MonoBehaviour
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


            if (health == 0)
            {
                // TODO: Fire death event
                Debug.Log("You killed it!");
            }
        }
    }
}
