using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    [RequireComponent(typeof(Inventory))]
    public class InventorySelector : MonoBehaviour
    {
        // Hold a reference to the inventory
        // ASSUMPTIONS: Inventory is NEVER empty. It is filled with empty item slots.
        [SerializeField]
        Inventory data;

        // Index of the current data
        // Starts at -1 as there is nothing in inventory
        [SerializeField]
        int index = 0; //SERIALIZE IS ONLY FOR DEBUGGING

        public void SwitchItemRight()
        {
            // Round back to 0 when at rightmost slot
            if(index + 1 >= data.GetInventoryItemSlots().Count)
            {
                index = 0;
            }
            else
            {
                // Increase index
                index++;
            }
        }

        public void SwitchItemLeft()
        {
            // Round back to count-1 when at leftmost slot
            if (index - 1 < 0)
            {
                index = data.GetInventoryItemSlots().Count - 1;
            }
            else
            {
                // Decrease index (moves left)
                index--;
            }
        }

        public ItemSlot QueryCurrentItemSlot()
        {
            return data.GetInventoryItemSlots()[index];
        }
    }
}
