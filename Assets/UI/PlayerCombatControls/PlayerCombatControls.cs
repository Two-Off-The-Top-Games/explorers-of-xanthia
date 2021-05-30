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
    }

    private void OnPlayerTurnStartedEvent(CharacterTurnStartedEvent _)
    {
        Debug.Log("Player Turn Started!");
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(true));
    }

    private void OnPlayerTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        EnqueueAction(() => PlayerCombatControlsPanel.SetActive(false));
    }
}
