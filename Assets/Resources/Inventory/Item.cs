using Events.Entity;
using Events.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public TextMeshProUGUI ItemText;
    public string Name;

    private Button _button;
    private bool _itemEnabled;

    private void Start()
    {
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
            new UseItemStartedEvent().Fire();
            ClickTargetSelectedEvent.RegisterListener(OnClickTargetSelectedEvent);
            new EnableClickTargetEvent().Fire();
        }
    }

    private void OnClickTargetSelectedEvent(ClickTargetSelectedEvent clickTargetSelectedEvent)
    {
        new DisableClickTargetEvent().Fire();
        ClickTargetSelectedEvent.DeregisterListener(OnClickTargetSelectedEvent);
        ItemEffect(clickTargetSelectedEvent.SelectedInstanceId);
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
