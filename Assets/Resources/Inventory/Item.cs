using Events.Entity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public TextMeshProUGUI ItemText;

    public string Name;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
        ItemText.text = Name;
    }

    private void OnButtonClicked()
    {
        ClickTargetSelectedEvent.RegisterListener(OnClickTargetSelectedEvent);
        new EnableClickTargetEvent().Fire();
    }

    private void OnClickTargetSelectedEvent(ClickTargetSelectedEvent clickTargetSelectedEvent)
    {
        new DisableClickTargetEvent().Fire();
        ClickTargetSelectedEvent.DeregisterListener(OnClickTargetSelectedEvent);
        ItemEffect(clickTargetSelectedEvent.SelectedInstanceId);
    }

    protected abstract void ItemEffect(int targetInstanceId);
}
