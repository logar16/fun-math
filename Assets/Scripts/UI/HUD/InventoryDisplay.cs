using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class InventoryDisplay : MonoBehaviour
    {
        private Inventory inventory;
        private List<ItemSlotDisplay> itemSlotDisplays;
        
        [SerializeField]
        private ItemSlotDisplay prototypeSlotDisplay;

        // Start is called before the first frame update
        void Start()
        {
            // TODO: Get the values from the inventory
            //GameObject.FindAnyObjectByType<PlayerController>.GetComponent<Inventory>().OnInventoryChanged += Inventory_OnInventoryChanged;
            itemSlotDisplays = new List<ItemSlotDisplay>();
            for (var i = 0; i < 4; i++)
            {
                Item item = new Item(); // Bad way to create a new item.
                item.thisItemType = (Item.ItemType)i;
                // Create a new item slot display
                var display = Instantiate(prototypeSlotDisplay, transform);
                display.ItemSlot = new ItemSlot(item, i);
                itemSlotDisplays.Add(display);
                if (i == 0)
                {
                    display.Select();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
