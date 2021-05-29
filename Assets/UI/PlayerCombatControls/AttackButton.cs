using Entities.Events;
using GameState.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    private Button _button;
    private int _enemyInstanceId;

    private void OnEnable()
    {
        EnemySpawnedEvent.RegisterListener(OnEnemySpawnedEvent);
    }

    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new CharacterAttackEvent(_enemyInstanceId).Fire();
    }

    private void OnEnemySpawnedEvent(EnemySpawnedEvent enemySpawnedEvent)
    {
        _enemyInstanceId = enemySpawnedEvent.EnemyInstanceId;
    }
}
