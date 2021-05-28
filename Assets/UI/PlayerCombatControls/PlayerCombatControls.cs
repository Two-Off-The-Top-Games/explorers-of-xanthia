using Entities.Events;
using UnityEngine;

public class PlayerCombatControls : MonoBehaviour
{
    private void OnEnable()
    {
        CharacterTurnStartedEvent.RegisterListener(OnPlayerTurnStartedEvent);
        CharacterTurnEndedEvent.RegisterListener(OnPlayerTurnEndedEvent);
    }

    private void OnPlayerTurnStartedEvent(CharacterTurnStartedEvent _)
    {
        gameObject.SetActive(true);
    }

    private void OnPlayerTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        gameObject.SetActive(false);
    }
}
