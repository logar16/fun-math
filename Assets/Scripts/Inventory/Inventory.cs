using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    // This class represents a slot within the player's inventory that holds an item
    [System.Serializable]
    public class ItemSlot
    {
        public ItemSlot(Item item, int Count)
        {
            this.item = item;
            this.ItemCount = Count;
        }
        [SerializeField]
        public Item item;
        public int ItemCount = 0;
        public bool Empty()
        {
            return ItemCount == 0;
        }
    }

    public class Inventory : MonoBehaviour
    {
        public int MaxNumberOfItem = 100;
        public bool InfiniteItem = false;
        public int MaxNumberOfTypes = (int)Item.ItemType.OperationMax; // By default goes to max number of operations

        private void Start()
        {
            Items = new List<ItemSlot>(MaxNumberOfTypes);
            // Regardless of items, we will always have this amount of inventory slots
            for(int i = 0; i < MaxNumberOfTypes; ++i)
            {
                Items.Add(new ItemSlot(null, 0));
            }
        }

        [SerializeField]
        private List<ItemSlot> Items ; // Holds the player's collected items
        public List<ItemSlot> GetInventoryItemSlots()
        {
            return Items;
        }

        public void AddNewItemToSlot(ItemSlot slot, Item item, int count)
        {
            // If the item slot does not contain an item yet
            if (slot.item == null)
            {
                // ASSMPUTION IS THAT WE ARE ALWAYS ADDING COUNT HERE
                slot.item = item;
                slot.ItemCount = count;
                return;
            }
            Debug.LogError("Attempting to add item to a non null item slot");
        }

        // Given an item slot, change its count
        public void ModifyItemSlot(ItemSlot slot, int count)
        {
            // Ensure that infinite is not on when we want to modify item count
            if (!this.InfiniteItem)
            {
                // Ensure we don't go over max
                if (MaxNumberOfItem > (slot.ItemCount + count))
                {
                    // Add count
                    slot.ItemCount += count;
                }
                // Ensure we don't go below 0
                else if (0 >= (slot.ItemCount + count))
                {
                    // Else we just go to 0
                    slot.ItemCount = 0;
                }
            }
        }

        public void ModifyItemSlot(Item item, int count)
        {
            ItemSlot slot = FindItem(item.thisItemType);

            if (slot != null)
                ModifyItemSlot(slot, count);
            else
            {
                // A little inefficient
                if(FindEmptySlot(out slot))
                {
                    AddNewItemToSlot(slot, item, count);
                }
                else
                {
                    // No empty slots found
                    Debug.LogWarning("No empty slots found to add item");
                }
            }
        }

        // Go through the inventory list to find if we already have the same type
        public ItemSlot FindItem(Item.ItemType itemType)
        {
            foreach (ItemSlot thisSlot in Items)
            {
                if (thisSlot.item?.thisItemType == itemType)
                {
                    return thisSlot;
                }
            }
            return null;
        }

        // Returns null when no slots are available
        public ItemSlot FindItemOrEmptySlot(Item.ItemType itemType)
        {
            ItemSlot emptySlot = null;
            foreach (ItemSlot thisSlot in Items)
            {
                if (thisSlot.item?.thisItemType == itemType)
                {
                    return thisSlot;
                }
                else if (thisSlot == null)
                    emptySlot = thisSlot;
            }
            return emptySlot;
        }

        // Returns true/false is there is an empty slot
        // Out function returns the empty slot
        public bool FindEmptySlot(out ItemSlot slot)
        {
            // Go through the inventory list to find if we already have the same type
            foreach (ItemSlot thisSlot in Items)
            {
                if (thisSlot.item == null)
                {
                    slot = thisSlot;
                    return true;
                }
            }
            slot = null;
            return false;
        }
    }
}