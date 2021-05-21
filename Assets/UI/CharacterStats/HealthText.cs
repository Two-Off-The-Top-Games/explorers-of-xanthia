using Entities.Events;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = gameObject.GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        EntityHealthChangedEvent.RegisterListener(UpdateHealthText);
    }

    private void OnDisable()
    {
        EntityHealthChangedEvent.DeregisterListener(UpdateHealthText);
    }

    private void UpdateHealthText(EntityHealthChangedEvent entityHealthChangedEvent)
    {
        _text.text = $"HP: {entityHealthChangedEvent.CurrentHealth}/{entityHealthChangedEvent.MaxHealth}";
    }
}
