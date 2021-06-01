using Entities;
using Entities.Events;
using UnityEngine;

public class PlayerCombatControls : UIComponentWithQueueableActions
{
    public GameObject PlayerCombatControlsPanel;

    private void OnEnable()
    {
        PlayerCombatControlsPanel.SetActive(false);
        CharacterTurnStartedEvent.RegisterListener(OnPlayerTurnStartedEvent);
        CharacterTurnEndedEvent.RegisterListener(OnPlayerTurnEndedEvent);
        CharacterAttackEvent.RegisterListener(OnCharacterAttackEvent);
        CharacterSelectedAttackTargetEvent.RegisterListener(OnCharacterSelectedAttackTargetEvent);
    }

    private void OnPlayerTurnStartedEvent(CharacterTurnStartedEvent _)
    {
        Debug.Log("Player Turn Started!");
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(true));
    }

    private void OnPlayerTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        Debug.Log("Player Turn Ended!");
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(false));
    }

    private void OnCharacterAttackEvent(CharacterAttackEvent _)
    {
        Debug.Log("Player Attacked!");
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(false));
    }

    private void OnCharacterSelectedAttackTargetEvent(CharacterSelectedAttackTargetEvent _)
    {
        Debug.Log("Player Selected Attack Target!");
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(true));
    }
}
