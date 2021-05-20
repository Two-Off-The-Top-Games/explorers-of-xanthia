using Character.Events;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        CharacterHealthChangedEvent.RegisterListener(UpdateHealthText);
        _text = gameObject.GetComponentInChildren<Text>();
    }

    private void UpdateHealthText(CharacterHealthChangedEvent characterHealthChangedEvent)
    {
        _text.text = $"HP: {characterHealthChangedEvent.CurrentHealth}/{characterHealthChangedEvent.MaxHealth}";
    }
}
