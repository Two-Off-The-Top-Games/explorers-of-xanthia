using Entities.Events;
using UnityEngine;
using UnityEngine.UI;

public class DealDamageButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = gameObject.GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new EntityTakeDamageEvent(1).Fire();
    }
}
