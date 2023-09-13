using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace FunMath
{
    public class ItemSlotDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text ItemName;
        [SerializeField]
        private TMP_Text ItemCount;

        private ItemSlot itemSlot;
        public ItemSlot ItemSlot
        {
            get
            {
                return itemSlot;
            }
            set
            {
                itemSlot = value;
                if (itemSlot != null)
                {
                    // Update the text to match the item's name
                    var options = new string[] {"-", "+", "÷", "x"};
                    ItemName.text = options[itemSlot.ItemCount];
                    ItemCount.text = itemSlot.ItemCount.ToString();
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
