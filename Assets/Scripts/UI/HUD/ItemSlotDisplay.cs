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
        private Transform highlightYellow;
        private Transform highlightBlue;
        private Transform highlightGreen;

        public enum HighlighColor
        {
            Yellow,
            Blue,
            Green
        }

        // Start is called before the first frame update
        void Awake()
        {
            highlightYellow = transform.Find("HighlightYellow");
            highlightBlue = transform.Find("HighlightBlue");
            highlightGreen = transform.Find("HighlightGreen");
        }

        public void Select(HighlighColor color = HighlighColor.Yellow)
        {
            switch (color)
            {
                case HighlighColor.Yellow:
                    highlightYellow.gameObject.SetActive(true);
                    break;
                case HighlighColor.Blue:
                    highlightBlue.gameObject.SetActive(true);
                    break;
                case HighlighColor.Green:
                    highlightGreen.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public void Deslect()
        {
            highlightYellow.gameObject.SetActive(false);
            highlightBlue.gameObject.SetActive(false);
            highlightGreen.gameObject.SetActive(false);
        }
    }
}
