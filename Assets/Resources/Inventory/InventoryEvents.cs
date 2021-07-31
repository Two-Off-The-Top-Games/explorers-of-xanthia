using UnityEngine;

namespace Events.Inventory
{
    public class AddItemToInventoryEvent : GlobalEvent<AddItemToInventoryEvent>
    {
        public AddItemToInventoryEvent(GameObject itemToAdd)
        {
            ItemToAdd = itemToAdd;
        }

        public GameObject ItemToAdd;
    }
}