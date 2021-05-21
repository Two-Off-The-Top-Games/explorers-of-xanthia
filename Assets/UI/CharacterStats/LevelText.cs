using Entities.Events;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = gameObject.GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        CharacterLevelChangedEvent.RegisterListener(UpdateLevelText);
    }

    private void OnDisable()
    {
        CharacterLevelChangedEvent.DeregisterListener(UpdateLevelText);
    }

    private void UpdateLevelText(CharacterLevelChangedEvent characterLevelChangedEvent)
    {
        _text.text = $"Level: {characterLevelChangedEvent.Level}";
    }
}
