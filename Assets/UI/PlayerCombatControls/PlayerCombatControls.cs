using Entities.Events;
using UnityEngine;

public class PlayerCombatControls : MonoBehaviour
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
        PlayerCombatControlsPanel.SetActive(true);
    }

    private void OnPlayerTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        PlayerCombatControlsPanel.SetActive(false);
    }
}
