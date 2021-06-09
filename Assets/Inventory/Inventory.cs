using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<int, GameObject> _items = new Dictionary<int, GameObject>();

    public void AddItem(GameObject item)
    {
        // Instantiate
        // Add instantiated item
        // Register listener(s)
        // Fire events
    }

    // TODO: Add event.
    private void OnRemoveItemFromInventoryEvent()
    {
        // Remove item from inventory
        // Deregister listener(s)
        // Destroy item
        // Fire events
    }
}
