using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents a slot within the player's inventory that holds an item
[System.Serializable]
public class ItemSlot
{
    public ItemSlot(Item item, int Count)
    {
        this.item = item;
        this.ItemCount = Count;
    }
    public Item item;
    public int ItemCount = 0;
}

public class Inventory : MonoBehaviour
{
    public int MaxNumberOfItem = 100;
    public bool InfiniteItem = false;

    [SerializeField]
    private List<ItemSlot> Items = new List<ItemSlot>(); // Holds the player's collected items
    public List<ItemSlot> GetInventoryItemSlots()
    {
        return Items;
    }

    public void ModifyItemSlot(Item item, int count)
    {
        ItemSlot slot = FindItem(item.thisItemType);

        if(slot != null) // Item already exists
        {
            // Ensure that infinite is not on when we want to modify item count
            if (!this.InfiniteItem)
            {
                // Ensure we don't go over max
                if(MaxNumberOfItem < (slot.ItemCount + count))
                {
                    // Add count
                    slot.ItemCount += count;
                }
            }
        }
        else // Item does not exist
        {
            // Add to list
            Items.Add(new ItemSlot(item, 2));
        }
    }

    public ItemSlot FindItem(Item.ItemType itemType)
    {
        // Go through the inventory list to find if we already have the same type
        foreach (ItemSlot thisSlot in Items)
        {
            if(thisSlot.item.thisItemType == itemType)
            {
                return thisSlot;
            }
        }
        return null;
    }
}
