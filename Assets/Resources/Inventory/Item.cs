using Events.Entity;
using Events.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public TextMeshProUGUI ItemText;
    public string Name;

    private int _instanceId;
    private Button _button;
    private bool _itemEnabled;

    private void Start()
    {
        _instanceId = GetInstanceID();
        CancelUseItemEvent.RegisterListener(_instanceId, OnEndUseItemEvent);
        UseItemStartedEvent.RegisterListener(OnUseItemStartedEvent);
        UseItemEndedEvent.RegisterListener(OnUseItemEndedEvent);
        _itemEnabled = true;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
        ItemText.text = Name;
    }

    private void OnDestroy()
    {
        UseItemStartedEvent.DeregisterListener(OnUseItemStartedEvent);
        UseItemEndedEvent.DeregisterListener(OnUseItemEndedEvent);
    }

    private void OnButtonClicked()
    {
        if (_itemEnabled)
        {
            new UseItemStartedEvent(_instanceId).Fire();
            ClickTargetSelectedEvent.RegisterListener(OnClickTargetSelectedEvent);
            new EnableClickTargetEvent().Fire();
        }
    }

    private void OnClickTargetSelectedEvent(ClickTargetSelectedEvent clickTargetSelectedEvent)
    {
        EndUseItem();
        ItemEffect(clickTargetSelectedEvent.SelectedInstanceId);
    }

    private void OnEndUseItemEvent(CancelUseItemEvent endUseItemEvent)
    {
        EndUseItem();
    }

    private void EndUseItem()
    {
        new DisableClickTargetEvent().Fire();
        ClickTargetSelectedEvent.DeregisterListener(OnClickTargetSelectedEvent);
        new UseItemEndedEvent().Fire();
    }

    private void OnUseItemStartedEvent(UseItemStartedEvent _)
    {
        _itemEnabled = false;
    }

    private void OnUseItemEndedEvent(UseItemEndedEvent _)
    {
        _itemEnabled = true;
    }

    protected abstract void ItemEffect(int targetInstanceId);
}
