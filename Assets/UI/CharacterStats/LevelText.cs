using Character.Events;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        CharacterLevelChangedEvent.RegisterListener(UpdateLevelText);
        _text = gameObject.GetComponentInChildren<Text>();
    }

    private void UpdateLevelText(CharacterLevelChangedEvent characterLevelChangedEvent)
    {
        _text.text = $"Level: {characterLevelChangedEvent.Level}";
    }
}
