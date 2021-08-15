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

    public class UseItemStartedEvent : GlobalEvent<UseItemStartedEvent>
    {
        public UseItemStartedEvent(int sourceItemInstanceId)
        {
            SourceItemInstanceId = sourceItemInstanceId;
        }

        public int SourceItemInstanceId;
    }

    public class UseItemEndedEvent : GlobalEvent<UseItemEndedEvent> { }

    public class CancelUseItemEvent : TargetEvent<CancelUseItemEvent>
    {
        public CancelUseItemEvent(int targetInstanceId) : base(targetInstanceId) { }
    }
}