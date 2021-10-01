using Events.Entity;
using GameState.Events;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private List<int> _enemies = new List<int>();
    private List<int> _characters = new List<int>();
    private Queue<int> _turnOrder = new Queue<int>();
    private HashSet<int> _deadEntities = new HashSet<int>();
    private int _activeTurn;

    private void Awake()
    {
        StartGameEvent.RegisterListener(OnStartGameEvent);
        CharacterSpawnedEvent.RegisterListener(OnCharacterSpawnedEvent);
        EnemySpawnedEvent.RegisterListener(OnEnemySpawnedEvent);
        EndTurnEvent.RegisterListener(OnEndTurnEvent);
    }

    private void OnDisable()
    {
        StartGameEvent.DeregisterListener(OnStartGameEvent);
        CharacterSpawnedEvent.DeregisterListener(OnCharacterSpawnedEvent);
        EnemySpawnedEvent.DeregisterListener(OnEnemySpawnedEvent);
        EndTurnEvent.DeregisterListener(OnEndTurnEvent);
    }

    private void OnStartGameEvent(StartGameEvent _)
    {
        new SpawnCharactersEvent(3).Fire();
        new SpawnEnemiesEvent(3).Fire();
        StartCombat();
    }

    private void OnCharacterSpawnedEvent(CharacterSpawnedEvent characterSpawnedEvent)
    {
        _characters.Add(characterSpawnedEvent.CharacterInstanceId);
        _turnOrder.Enqueue(characterSpawnedEvent.CharacterInstanceId);
        EntityDiedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnEntityDiedEvent);
    }

    private void OnEnemySpawnedEvent(EnemySpawnedEvent enemySpawnedEvent)
    {
        _enemies.Add(enemySpawnedEvent.EnemyInstanceId);
        _turnOrder.Enqueue(enemySpawnedEvent.EnemyInstanceId);
        EntityDiedEvent.RegisterListener(enemySpawnedEvent.EnemyInstanceId, OnEntityDiedEvent);
    }

    private void StartCombat()
    {
        StartNextTurn();
    }

    private void OnEndTurnEvent(EndTurnEvent _)
    {
        EndActiveTurn();
    }

    private void StartNextTurn()
    {
        int participant = _turnOrder.Dequeue();
        if (_deadEntities.Contains(participant))
        {
            _deadEntities.Remove(participant);
            StartNextTurn();
            return;
        }
        _activeTurn = participant;
        new StartCombatTurnEvent(_activeTurn).Fire();
    }

    private void EndActiveTurn()
    {
        new TurnEndedEvent(_activeTurn).Fire();
        _turnOrder.Enqueue(_activeTurn);
        StartNextTurn();
    }

    private void OnEntityDiedEvent(EntityDiedEvent entityDiedEvent)
    {
        _deadEntities.Add(entityDiedEvent.SourceInstanceId);
        EntityDiedEvent.DeregisterListener(entityDiedEvent.SourceInstanceId, OnEntityDiedEvent);
    }
}
