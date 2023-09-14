using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class ModifierInventoryDisplay : MonoBehaviour
    {
        private InventorySelector<ModifierItem> selector;
        private List<ItemSlotDisplay> itemSlotDisplays;
        
        [SerializeField]
        private ItemSlotDisplay prototypeSlotDisplay;

        [SerializeField]
        private ItemSlotDisplay.HighlighColor highlighColor = ItemSlotDisplay.HighlighColor.Yellow;

        // Start is called before the first frame update
        void Awake()
        {
            selector = FindAnyObjectByType<PlayerController>().ModifierSelector;
            selector.OnSelectionChange += OnSelectionChange;
            var inventory = selector.GetInventory();
            var items = inventory.GetItems();
            itemSlotDisplays = new List<ItemSlotDisplay>();
            foreach (var item in items)
            {
                var display = Instantiate(prototypeSlotDisplay, transform);
                display.ItemData = item;
                itemSlotDisplays.Add(display);
            }

            if (itemSlotDisplays.Count > 0)
                itemSlotDisplays[0].Select(highlighColor);
        }

        private void OnEnable()
        {
            selector = selector ?? FindAnyObjectByType<PlayerController>().ModifierSelector;
            selector.OnSelectionChange += OnSelectionChange;
            var inventory = selector.GetInventory();
            inventory.OnInventoryChanged += OnInventoryChanged;
        }

        private void OnDisable()
        {
            selector.OnSelectionChange -= OnSelectionChange;
            var inventory = selector.GetInventory();
            inventory.OnInventoryChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(List<ModifierItem> Items)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                itemSlotDisplays[i].ItemData = Items[i];
            }
        }

        private void OnSelectionChange(int index)
        {
            foreach (var display in itemSlotDisplays)
            {
                display.Deslect();
            }
            itemSlotDisplays[index].Select(highlighColor);
        }
    }
}
