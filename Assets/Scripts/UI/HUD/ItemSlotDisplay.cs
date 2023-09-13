using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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

        // Get the ui image child object
        private Transform highlight;

        // Start is called before the first frame update
        void Awake()
        {
            highlight = transform.Find("Highlight");
        }

        public void Select()
        {
            highlight.gameObject.SetActive(true);
        }

        public void Deslect()
        {
            highlight.gameObject.SetActive(false);
        }
    }
}
