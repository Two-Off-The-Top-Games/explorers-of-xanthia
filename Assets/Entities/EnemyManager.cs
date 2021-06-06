using Entities.Events;
using GameState.Events;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    [Range(0.01f, 100f)]
    public float PercentOfAvailableVerticalScreenSpace;
    private Dictionary<int, GameObject> _spawnedEnemies = new Dictionary<int, GameObject>();
    private RectTransform _rectTransform;

    private void OnEnable()
    {
        SpawnEnemiesEvent.RegisterListener(OnSpawnEnemiesEvent);
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnSpawnEnemiesEvent(SpawnEnemiesEvent spawnEnemiesEvent)
    {
        float spawnLocationYOffset = _rectTransform.rect.height / (spawnEnemiesEvent.NumberToSpawn + 1);
        float scaledSpawnLocationYOffest = spawnLocationYOffset * (PercentOfAvailableVerticalScreenSpace / 100f);
        float topOfContainer = _rectTransform.rect.height / 2;
        float spawnLocationXCoordinate = _rectTransform.rect.width / 4;
        for (int i = 0; i < spawnEnemiesEvent.NumberToSpawn; i++)
        {
            float spawnLocationYCoordinate = topOfContainer - ((i + 1) * scaledSpawnLocationYOffest);
            var spawnedEnemy = Instantiate(EnemyPrefab, new Vector3(spawnLocationXCoordinate, spawnLocationYCoordinate), Quaternion.identity, transform);
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
