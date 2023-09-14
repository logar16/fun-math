using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class InventorySelector<T> where T : Item
    {
        // Hold a reference to the inventory
        // ASSUMPTIONS: Inventory is NEVER empty. It is filled with empty item slots.
        Inventory<T> data;
        public Inventory<T> GetInventory() { return data; }

        // Index of the current data
        int index = 0;

        // Event for when selection changed
        public delegate void SelectionChanged(int index);
        public event SelectionChanged OnSelectionChange;


        public InventorySelector(int maxNumberOfTypes)
        {
            data = new Inventory<T>(maxNumberOfTypes);
        }

        public void SelectIndex(int index)
        {
            if (index < 0 || index >= data.GetFilledSlotsCount())
                return;
            this.index = index;
            OnSelectionChange?.Invoke(index);
        }

        public void SwitchItemRight()
        {
            // Round back to 0 when at rightmost slot
            if(index + 1 >= data.GetFilledSlotsCount())
            {
                index = 0;
            }
            else
            {
                // Increase index
                index++;
            }
            OnSelectionChange?.Invoke(index);
        }

        public void SwitchItemLeft()
        {
            index--;
            // Round back to count-1 when at leftmost slot
            if (index < 0)
            {
                // If we don't have anything in our slots yet, we leave at zero.
                index = Mathf.Max(0, data.GetFilledSlotsCount() - 1);
            }
            OnSelectionChange?.Invoke(index);
        }

        public T QueryCurrentItem()
        {
            return data.GetItemAt(index);
        }

        public bool CanUseCurrentItem()
        {
            var item = data.GetItemAt(index);
            return item != null && item.Count > 0;
        }

        public void UseCurrentItem()
        {
            if (CanUseCurrentItem())
                data.ModifyItemAt(index, -1);
        }

        public void AddItem(T item)
        {
            data.ModifyItemSlot(item);
        }
    }
}
