using GameState.Events;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private List<int> _combatParticipants = new List<int>();
    private void OnEnable()
    {
        StartGameEvent.RegisterListener(OnStartGameEvent);
        CharacterSpawnedEvent.RegisterListener(OnCharacterSpawnedEvent);
        EnemySpawnedEvent.RegisterListener(OnEnemySpawnedEvent);
    }

    private void OnStartGameEvent(StartGameEvent _)
    {
        new SpawnCharactersEvent(3).Fire();
        new SpawnEnemiesEvent(3).Fire();
        new StartCombatEvent(_combatParticipants.ToArray()).Fire();
    }

    private void OnCharacterSpawnedEvent(CharacterSpawnedEvent characterSpawnedEvent)
    {
        _combatParticipants.Add(characterSpawnedEvent.CharacterInstanceId);
    }

    private void OnEnemySpawnedEvent(EnemySpawnedEvent enemySpawnedEvent)
    {
        _combatParticipants.Add(enemySpawnedEvent.EnemyInstanceId);
    }
}
