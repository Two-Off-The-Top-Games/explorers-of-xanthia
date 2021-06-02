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
    }

    private void OnDisable()
    {
        CharacterSelectedAttackTargetEvent.DeregisterListener(OnCharacterSelectedAttackTargetEvent);
        CharacterHealthChangedEvent.DeregisterListener(OnCharacterHealthChangedEvent);
    }

    private void OnCharacterSelectedAttackTargetEvent(CharacterSelectedAttackTargetEvent characterSelectedAttackTargetEvent)
    {
        new CombatEvent("Player attacked an enemy.").Fire();
    }

    private void OnCharacterHealthChangedEvent(CharacterHealthChangedEvent characterHealthChangedEvent)
    {
        string gainedOrLost = characterHealthChangedEvent.Change > 0 ? "gained" : "lost";
        new CombatEvent($"Player {gainedOrLost} {Math.Abs(characterHealthChangedEvent.Change)} health.").Fire();
    }
}
