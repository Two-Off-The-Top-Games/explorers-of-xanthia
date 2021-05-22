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
        CharacterHealthChangedEvent.RegisterListener(UpdateHealthText);
    }

    private void OnDisable()
    {
        CharacterHealthChangedEvent.DeregisterListener(UpdateHealthText);
    }

    private void UpdateHealthText(CharacterHealthChangedEvent characterHealthChangedEvent)
    {
        _text.text = $"HP: {characterHealthChangedEvent.CurrentHealth}/{characterHealthChangedEvent.MaxHealth}";
    }
}
