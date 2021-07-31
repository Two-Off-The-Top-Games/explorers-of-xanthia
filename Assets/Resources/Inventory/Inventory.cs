using Events.Inventory;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private void Awake()
    {
        AddItemToInventoryEvent.RegisterListener(AddItemToInventory);
    }

    private void AddItemToInventory(AddItemToInventoryEvent addItemToInventoryEvent)
    {
        Instantiate(addItemToInventoryEvent.ItemToAdd, transform);
    }
}
