using System;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class Roland_TestScript : MonoBehaviour
    {
        InventorySelector<ModifierItem> ModifierSelector;
        InventorySelector<OperationItem> OperatorSelector;
        // Start is called before the first frame update
        void Start()
        {
            var player = FindObjectOfType<PlayerController>();
            ModifierSelector = player.GetModifierSelector();
            ModifierSelector.OnSelectionChange += OnSelectionChanged;
            ModifierSelector.GetInventory().OnInventoryChanged += OnModInventoryChanged;
            OperatorSelector = player.GetOperationSelector();
            OperatorSelector.OnSelectionChange += OnSelectionChanged;
            OperatorSelector.GetInventory().OnInventoryChanged += OnOpInventoryChanged;
        }

        private void OnOpInventoryChanged(List<OperationItem> Items)
        {
            foreach (var Item in Items)
            {
                if (Item != null)
                    Debug.Log(Item.Operator.ToString());
            }
        }

        private void OnModInventoryChanged(List<ModifierItem> Items)
        {
            foreach(var Item in Items)
            {
                if (Item != null)
                Debug.Log($"Modifier: {Item.Modifier}");
            }
        }

        private void OnSelectionChanged(int index)
        {
            Debug.Log("Selection changed to " + index);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyUp(KeyCode.Q))
            {
                ModifierSelector.SwitchItemLeft();
            }
            else if(Input.GetKeyUp(KeyCode.E))
            {
                ModifierSelector.SwitchItemRight();
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                var ItemSlot = ModifierSelector.QueryCurrentItem();
                
                if(ItemSlot != null)
                {
                    Debug.Log("Current item is " + ItemSlot.ToString());
                    if (!(ItemSlot.Count > 0))
                    {
                        Debug.Log("Using Item: " + ItemSlot.ToString());
                    }
                    else
                    {
                        Debug.Log("No more item of type: " + ItemSlot.ToString());
                    }
                }
                else
                {
                    Debug.Log("Current slot is empty?");
                }
            }
        }
    }
}
