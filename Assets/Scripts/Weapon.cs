using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class Weapon : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject arrowPrefab;

        void ShootArrow()
        {
            // TODO: Use real-time setting when inventory is ready
            //
            //PlayerController player = gameObject.GetComponent<PlayerController>();
            //OperationType operation = player.GetOperationSelector().QueryCurrentItem().Operator;
            //int modifier = player.GetModifierSelector().QueryCurrentItem().Modifier;

            OperationType operation = OperationType.Subtraction;
            int modifier = 10;

            Arrow arrow = arrowPrefab.GetComponent<Arrow>();
            arrow.Operator = operation;
            arrow.Modifier = modifier;

            Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
