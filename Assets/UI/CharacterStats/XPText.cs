using Character.Events;
using UnityEngine;
using UnityEngine.UI;

public class XPText : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        CharacterXPChangedEvent.RegisterListener(UpdateXPText);
        _text = gameObject.GetComponentInChildren<Text>();
    }

    private void UpdateXPText(CharacterXPChangedEvent characterXPChangedEvent)
    {
        _text.text = $"XP: {characterXPChangedEvent.XP}";
    }
}
