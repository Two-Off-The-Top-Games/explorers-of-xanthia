using Events.Inventory;
using Events.UI;
using UnityEngine;

public class CancelItemManager : MonoBehaviour
{
    private int _activeItemInUse;

    private void Start()
    {
        UseItemStartedEvent.RegisterListener(OnUseItemStartedEvent);
        CancelButtonClickedEvent.RegisterListener(OnCancelButtonClickedEvent);
    }

    private void OnUseItemStartedEvent(UseItemStartedEvent useItemStartedEvent)
    {
        _activeItemInUse = useItemStartedEvent.SourceItemInstanceId;
    }

    private void OnCancelButtonClickedEvent(CancelButtonClickedEvent _)
    {
        new CancelUseItemEvent(_activeItemInUse).Fire();
    }
}
