using Entities.Events;
using GameState.Events;
using System;
using UnityEngine;

public class CombatEventLogConverter : MonoBehaviour
{
    private void OnEnable()
    {
        CharacterSelectedAttackTargetEvent.RegisterListener(OnCharacterSelectedAttackTargetEvent);
        CharacterHealthChangedEvent.RegisterListener(OnCharacterHealthChangedEvent);
        EndTurnEvent.RegisterListener(OnEndTurnEvent);
        CharacterActionPointsChangedEvent.RegisterListener(OnCharacterActionPointsChangedEvent);
        CharacterTakeDamageEvent.RegisterListener(OnCharacterTakeDamageEvent);
        CharacterTurnStartedEvent.RegisterListener(OnCharacterTurnStartedEvent);
        CharacterTurnEndedEvent.RegisterListener(OnCharacterTurnEndedEvent);
    }

    private void OnDisable()
    {
        CharacterSelectedAttackTargetEvent.DeregisterListener(OnCharacterSelectedAttackTargetEvent);
        CharacterHealthChangedEvent.DeregisterListener(OnCharacterHealthChangedEvent);
        EndTurnEvent.DeregisterListener(OnEndTurnEvent);
        CharacterActionPointsChangedEvent.DeregisterListener(OnCharacterActionPointsChangedEvent);
        CharacterTakeDamageEvent.DeregisterListener(OnCharacterTakeDamageEvent);
        CharacterTurnStartedEvent.DeregisterListener(OnCharacterTurnStartedEvent);
        CharacterTurnEndedEvent.DeregisterListener(OnCharacterTurnEndedEvent);
    }

    private void OnCharacterSelectedAttackTargetEvent(CharacterSelectedAttackTargetEvent characterSelectedAttackTargetEvent)
    {
        new CombatEvent("Character attacked an enemy.").Fire();
    }

    private void OnCharacterHealthChangedEvent(CharacterHealthChangedEvent characterHealthChangedEvent)
    {
        // TODO: Change this when the event changes to have previous and new values.
        new CombatEvent($"Character health changed to {characterHealthChangedEvent.CurrentHealth} health.").Fire();
    }

    private void OnEndTurnEvent(EndTurnEvent _)
    {
        new CombatEvent($"A turn has ended.").Fire();
    }

    private void OnCharacterActionPointsChangedEvent(CharacterActionPointsChangedEvent characterActionPointsChangedEvent)
    {
        // TODO: Change this when the event changes to have previous and new values.
        new CombatEvent($"Character action points changed to {characterActionPointsChangedEvent.CurrentActionPoints}.").Fire();
    }

    private void OnCharacterTakeDamageEvent(CharacterTakeDamageEvent characterTakeDamageEvent)
    {
        new CombatEvent($"Character is taking {characterTakeDamageEvent.Damage} damage.").Fire();
    }

    private void OnCharacterTurnStartedEvent(CharacterTurnStartedEvent _)
    {
        new CombatEvent("Character turn started.").Fire();
    }

    private void OnCharacterTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        new CombatEvent("Character turn ended.").Fire();
    }
}
