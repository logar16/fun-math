using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class to hold all types of items
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        // These are the type of items you can pick up, aka our math 'operators'
        Addition,
        Subtraction,
        Multiply,
        Divide,
        None
    }
    [SerializeField]
    public ItemType thisItemType;
}
