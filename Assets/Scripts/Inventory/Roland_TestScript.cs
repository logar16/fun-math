using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class Roland_TestScript : MonoBehaviour
    {
        [SerializeField]
        InventorySelector InventorySelector;
        [SerializeField]
        Inventory Inventory;
        // Start is called before the first frame update
        void Start()
        {
        
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
                var ItemSlot = InventorySelector.QueryCurrentItemSlot();
                
                if(ItemSlot.item != null)
                {
                    Debug.Log("Current item is " + ItemSlot.item.ToString());
                    if (!ItemSlot.Empty())
                    {
                        Debug.Log("Using Item: " + ItemSlot.item.ToString());
                        Inventory.ModifyItemSlot(ItemSlot, -1);
                    }
                    else
                    {
                        Debug.Log("No more item of type: " + ItemSlot.item.ToString());
                    }
                }
                else
                {
                    Debug.Log("Current item is empty");
                }
            }
        }
    }
}
