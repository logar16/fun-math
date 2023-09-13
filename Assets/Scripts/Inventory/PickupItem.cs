using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    [RequireComponent(typeof(Item))]
    public class PickupItem : MonoBehaviour
    {
        [SerializeField]
        private Item item;
        void Start()
        {
            item = GetComponent<Item>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            var inventory = other.gameObject.GetComponent<Inventory>();
            if (inventory != null)
            {
                Pickup(inventory);
            }
        }

        public void Pickup(Inventory inventory)
        {
            inventory.ModifyItemSlot(item, 1);

            // Destroying the object destroys reference in C#. The 'right'
            // move is to abstract out the item from monobehavior so it can live without a gameobject
            // Quick fix is to move this object to a place the player can never find
            transform.position = new Vector3(9999, 9999, 9999);
        }
    }
}
