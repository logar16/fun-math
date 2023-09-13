using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FunMath
{
    public class Inventory<T> where T : Item
    {
        public int MaxNumberOfItem = 100;
        public bool InfiniteItem = false;
        
        private List<T> Items;

        // Event for when the inventory changes
        public delegate void InventoryChanged(List<T> Items);
        public event InventoryChanged OnInventoryChanged;

        public Inventory(int maxNumberOfTypes = 5)
        {
            Items = new List<T>(maxNumberOfTypes);
        }

        public int GetFilledSlotsCount()
        {
            return Items.Count(item => item != null);
        }

        public T GetItemAt(int index)
        {
            if (index < 0 || index >= Items.Count)
            {
                return default(T);
            }
            return Items[index];
        }

        public bool EmptySlotsAvailable()
        {
            return Items.LastOrDefault() == null;
        }

        private void FillEmptySlot(T item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == null)
                {
                    Items[i] = item;
                    return;
                }
            }
        }

        /// <summary>
        /// Add the item to the inventory according to its count.
        /// If no such item is in the inventory, we add a new entry/slot
        /// </summary>
        /// <param name="item">The item definition and count to add</param>
        public void ModifyItemSlot(T item)
        {
            var slot = FindItem(item);

            if (slot != null)
            {
                var result = item.Count + slot.Count;
                if (result > MaxNumberOfItem && !InfiniteItem)
                {
                    slot.Count = MaxNumberOfItem;
                    //item.Count = result - MaxNumberOfItem;  //If you wanted to leave some behind...
                }
                else if (result < 0)
                {
                    slot.Count = 0;
                    // TODO: Remove item from inventory altogether?
                }
                else
                {
                    slot.Count = result;
                    //item.Count = 0;
                }
            }
            else
            {
                if (EmptySlotsAvailable())
                {
                    FillEmptySlot(item);
                }
                else
                {
                    // No empty slots found
                    Debug.LogWarning("No empty slots found to add item");
                }
            }
            // emit event after everything is done
            OnInventoryChanged?.Invoke(Items.ToList());
        }

        /// <summary>
        /// Go through the inventory list to find if we already have the same type
        /// </summary>
        /// <param name="query">Item to to match</param>
        /// <returns></returns>        
        public T FindItem(T query)
        {
            return Items.FirstOrDefault(item => item.Equals(query));
        }

        internal void ModifyItemAt(int index, int count)
        {
            var item = GetItemAt(index);
            if (item != null)
            {
                item.Count += count;
                OnInventoryChanged?.Invoke(Items.ToList());
            }
        }
    }
}