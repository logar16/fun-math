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
            Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
