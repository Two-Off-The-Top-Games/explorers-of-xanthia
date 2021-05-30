using GameState.Events;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private List<int> _combatParticipants = new List<int>();
    private void OnEnable()
    {
        StartGameEvent.RegisterListener(OnStartGameEvent);
        EnemySpawnedEvent.RegisterListener(OnEnemySpawnedEvent);
    }

    private void OnStartGameEvent(StartGameEvent _)
    {
        Debug.Log($"Character Instance Id: {Globals.Instance.CharacterInstanceId}");
        _combatParticipants.Add(Globals.Instance.CharacterInstanceId);
        new SpawnEnemiesEvent(1).Fire();
        new StartCombatEvent(_combatParticipants.ToArray()).Fire();
    }

    private void OnEnemySpawnedEvent(EnemySpawnedEvent enemySpawnedEvent)
    {
        _combatParticipants.Add(enemySpawnedEvent.EnemyInstanceId);
    }
}
