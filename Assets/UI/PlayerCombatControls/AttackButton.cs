using Entities.Events;
using GameState.Events;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : UIComponentWithQueueableActions
{
    private Button _button;

    private void OnEnable()
    {
        _button = gameObject.GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new CharacterAttackEvent().Fire();
    }

    private void OnCharacterActionPointsChangedEvent(CharacterActionPointsChangedEvent characterActionPointsChangedEvent)
    {
        EnqueueAction(() => _button.interactable = characterActionPointsChangedEvent.CurrentActionPoints > 0);
    }

    private void OnCharacterSpawnedEvent(CharacterSpawnedEvent characterSpawnedEvent)
    {
        CharacterTurnStartedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterTurnStartedEvent);
        CharacterTurnEndedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterTurnEndedEvent);
    }

    private void OnCharacterTurnStartedEvent(CharacterTurnStartedEvent characterTurnStartedEvent)
    {
        CharacterActionPointsChangedEvent.RegisterListener(characterTurnStartedEvent.SourceInstanceId, OnCharacterActionPointsChangedEvent);
    }

    private void OnCharacterTurnEndedEvent(CharacterTurnEndedEvent characterTurnEndedEvent)
    {
        CharacterActionPointsChangedEvent.DeregisterListener(characterTurnEndedEvent.SourceInstanceId, OnCharacterActionPointsChangedEvent);
    }
}
