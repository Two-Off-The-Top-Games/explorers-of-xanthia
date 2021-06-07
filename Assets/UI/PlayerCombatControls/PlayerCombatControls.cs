using Entities.Events;
using GameState.Events;
using UnityEngine;

public class PlayerCombatControls : UIComponentWithQueueableActions
{
    public GameObject PlayerCombatControlsPanel;

    private void OnEnable()
    {
        PlayerCombatControlsPanel.SetActive(false);
        CharacterSpawnedEvent.RegisterListener(OnCharacterSpawnedEvent);
    }

    private void OnCharacterSpawnedEvent(CharacterSpawnedEvent characterSpawnedEvent)
    {
        CharacterTurnStartedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterTurnStartedEvent);
        CharacterTurnEndedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterTurnEndedEvent);
        CharacterStartedAttackEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterStartedAttackEvent);
        CharacterFinishedAttackEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterFinishedAttackEvent);
    }

    private void OnCharacterTurnStartedEvent(CharacterTurnStartedEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(true));
    }

    private void OnCharacterTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(false));
    }

    private void OnCharacterStartedAttackEvent(CharacterStartedAttackEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(false));
    }

    private void OnCharacterFinishedAttackEvent(CharacterFinishedAttackEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(true));
    }
}
