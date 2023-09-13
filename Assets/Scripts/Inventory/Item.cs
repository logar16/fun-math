using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    // Base class to hold all types of items
    [System.Serializable]
    public class Item : MonoBehaviour
    {
        public enum ItemType
        {
            // These are the type of items you can pick up, aka our math 'operators'
            Addition = 0,
            Subtraction,
            Multiply,
            Divide,

            OperationMax,

            Number,
            None
        }
        [SerializeField]
        public ItemType thisItemType;
    }
}
