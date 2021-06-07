using Entities.Events;
using GameState.Events;
using UnityEngine;

public class CombatEventLogConverter : MonoBehaviour
{
    private void OnEnable()
    {
        CharacterSpawnedEvent.RegisterListener(OnCharacterSpawnedEvent);
        CharacterSelectedAttackTargetEvent.RegisterListener(OnCharacterSelectedAttackTargetEvent);
        EndTurnEvent.RegisterListener(OnEndTurnEvent);
        CharacterTurnStartedEvent.RegisterListener(OnCharacterTurnStartedEvent);
        CharacterTurnEndedEvent.RegisterListener(OnCharacterTurnEndedEvent);
    }

    private void OnDisable()
    {
        CharacterSelectedAttackTargetEvent.DeregisterListener(OnCharacterSelectedAttackTargetEvent);
        EndTurnEvent.DeregisterListener(OnEndTurnEvent);
        CharacterTurnStartedEvent.DeregisterListener(OnCharacterTurnStartedEvent);
        CharacterTurnEndedEvent.DeregisterListener(OnCharacterTurnEndedEvent);
    }

    private void OnCharacterSpawnedEvent(CharacterSpawnedEvent characterSpawnedEvent)
    {
        CharacterDiedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterDiedEvent);
        CharacterHealthChangedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterHealthChangedEvent);
        CharacterActionPointsChangedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterActionPointsChangedEvent);
    }

    private void OnCharacterDiedEvent(CharacterDiedEvent characterDiedEvent)
    {
        CharacterDiedEvent.DeregisterListener(characterDiedEvent.SourceInstanceId, OnCharacterDiedEvent);
        CharacterHealthChangedEvent.DeregisterListener(characterDiedEvent.SourceInstanceId, OnCharacterHealthChangedEvent);
        CharacterActionPointsChangedEvent.DeregisterListener(characterDiedEvent.SourceInstanceId, OnCharacterActionPointsChangedEvent);
    }

    private void OnCharacterSelectedAttackTargetEvent(CharacterSelectedAttackTargetEvent characterSelectedAttackTargetEvent)
    {
        new CombatEvent("Character attacked an enemy.").Fire();
    }

    private void OnCharacterHealthChangedEvent(CharacterHealthChangedEvent characterHealthChangedEvent)
    {
        if (characterHealthChangedEvent.PreviousHealth < characterHealthChangedEvent.CurrentHealth)
        {
            int change = characterHealthChangedEvent.CurrentHealth - characterHealthChangedEvent.PreviousHealth;
            new CombatEvent($"Character gained {change} health.");
        }
        else if (characterHealthChangedEvent.PreviousHealth > characterHealthChangedEvent.CurrentHealth)
        {
            int change = characterHealthChangedEvent.PreviousHealth - characterHealthChangedEvent.CurrentHealth;
            new CombatEvent($"Character lost {change} health.");
        }
        else if (characterHealthChangedEvent.PreviousMaxHealth < characterHealthChangedEvent.MaxHealth)
        {
            int change = characterHealthChangedEvent.MaxHealth - characterHealthChangedEvent.PreviousMaxHealth;
            new CombatEvent($"Character gained {change} max health.");
        }
        else if (characterHealthChangedEvent.PreviousMaxHealth < characterHealthChangedEvent.MaxHealth)
        {
            int change = characterHealthChangedEvent.PreviousMaxHealth - characterHealthChangedEvent.MaxHealth;
            new CombatEvent($"Character lost {change} max health.");
        }
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

    private void OnCharacterTurnStartedEvent(CharacterTurnStartedEvent _)
    {
        new CombatEvent("Character turn started.").Fire();
    }

    private void OnCharacterTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        new CombatEvent("Character turn ended.").Fire();
    }
}
