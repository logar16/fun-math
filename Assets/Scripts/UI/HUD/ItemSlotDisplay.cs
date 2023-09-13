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

        private Item item;
        public Item ItemData
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                if (item != null)
                {
                    // Update the text to match the item's name
                    ItemName.text = item.Name;
                    ItemCount.text = item.Count.ToString();
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
