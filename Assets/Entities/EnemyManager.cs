using Entities.Events;
using GameState.Events;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private Dictionary<int, GameObject> _spawnedEnemies = new Dictionary<int, GameObject>();

    private void OnEnable()
    {
        SpawnEnemiesEvent.RegisterListener(OnSpawnEnemiesEvent);
    }

    private void OnSpawnEnemiesEvent(SpawnEnemiesEvent spawnEnemiesEvent)
    {
        for(int i = 0; i < spawnEnemiesEvent.NumberToSpawn; i++)
        {
            var spawnedEnemy = Instantiate(EnemyPrefab, transform);
            var enemyInstanceId = spawnedEnemy.GetInstanceID();
            _spawnedEnemies.Add(enemyInstanceId, spawnedEnemy);
            EnemyDiedEvent.RegisterListener(enemyInstanceId, OnEnemyDiedEvent);
            new EnemySpawnedEvent(enemyInstanceId).Fire();
        }
    }

    private void OnEnemyDiedEvent(EnemyDiedEvent enemyDiedEvent)
    {
        if (_spawnedEnemies.TryGetValue(enemyDiedEvent.SourceInstanceId, out var spawnedEnemy))
        {
            Destroy(spawnedEnemy);
            _spawnedEnemies.Remove(enemyDiedEvent.SourceInstanceId);
            EnemyDiedEvent.DeregisterListener(enemyDiedEvent.SourceInstanceId, OnEnemyDiedEvent);
        }
    }
}
