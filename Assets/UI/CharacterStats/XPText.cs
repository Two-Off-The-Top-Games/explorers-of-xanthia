using Entities.Events;
using UnityEngine;
using UnityEngine.UI;

public class XPText : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = gameObject.GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        CharacterXPChangedEvent.RegisterListener(UpdateXPText);
    }

    private void OnDisable()
    {
        CharacterXPChangedEvent.DeregisterListener(UpdateXPText);
    }

    private void UpdateXPText(CharacterXPChangedEvent characterXPChangedEvent)
    {
        _text.text = $"XP: {characterXPChangedEvent.XP}";
    }
}
