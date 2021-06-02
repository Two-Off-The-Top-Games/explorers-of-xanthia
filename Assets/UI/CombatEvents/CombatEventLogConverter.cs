using Entities.Events;
using GameState.Events;
using UnityEngine;

public class CombatEventLogConverter : MonoBehaviour
{
    private void OnEnable()
    {
        CharacterSelectedAttackTargetEvent.RegisterListener(OnCharacterSelectedAttackTargetEvent);
    }

    private void OnCharacterSelectedAttackTargetEvent(CharacterSelectedAttackTargetEvent characterSelectedAttackTargetEvent)
    {
        new CombatEvent($"Player attacked an enemy.").Fire();
    }
}
