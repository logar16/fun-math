using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace FunMath
{
    [RequireComponent(typeof(Item))]
    public class PickupItem<T> : MonoBehaviour where T : Item
    {
        [SerializeField]
        private T item;
        void Start()
        {
            item = GetComponent<T>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            var selector = other.gameObject.GetComponent<InventorySelector<T>>();
            if (selector != null)
            {
                Pickup(selector);
            }
        }

        public void Pickup(InventorySelector<T> inventory)
        {
            inventory.AddItem(item);

            // Destroying the object destroys reference in C#. The 'right'
            // move is to abstract out the item from monobehavior so it can live without a gameobject
            // Quick fix is to move this object to a place the player can never find
            transform.position = new Vector3(9999, 9999, 9999);
        }
    }
}
