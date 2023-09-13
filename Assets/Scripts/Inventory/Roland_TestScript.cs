using System;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class Roland_TestScript : MonoBehaviour
    {
        InventorySelector<ModifierItem> InventorySelector;
        // Start is called before the first frame update
        void Start()
        {
            InventorySelector = FindObjectOfType<PlayerController>().GetModifierInventory();
            InventorySelector.OnSelectionChange += OnSelectionChanged;
            InventorySelector.GetInventory().OnInventoryChanged += OnInventoryChanged;
        }

        private void OnInventoryChanged(List<ModifierItem> Items)
        {
            foreach(var Item in Items)
            {
                Debug.Log(Item.ToString());
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
                InventorySelector.SwitchItemLeft();
            }
            else if(Input.GetKeyUp(KeyCode.E))
            {
                InventorySelector.SwitchItemRight();
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                var ItemSlot = InventorySelector.QueryCurrentItem();
                
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
