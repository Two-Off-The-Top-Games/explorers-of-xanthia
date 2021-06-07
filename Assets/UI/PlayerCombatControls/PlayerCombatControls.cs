using Entities;
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
        CharacterAttackEvent.RegisterListener(OnCharacterAttackEvent);
        CharacterSelectedAttackTargetEvent.RegisterListener(OnCharacterSelectedAttackTargetEvent);
    }

    private void OnCharacterSpawnedEvent(CharacterSpawnedEvent characterSpawnedEvent)
    {
        CharacterTurnStartedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterTurnStartedEvent);
        CharacterTurnEndedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterTurnEndedEvent);
    }

    private void OnCharacterTurnStartedEvent(CharacterTurnStartedEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(true));
    }

    private void OnCharacterTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(false));
    }

    private void OnCharacterAttackEvent(CharacterAttackEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(false));
    }

    private void OnCharacterSelectedAttackTargetEvent(CharacterSelectedAttackTargetEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(true));
    }
}
