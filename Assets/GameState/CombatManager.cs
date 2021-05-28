using Events.GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Move somewhere else and figure out the events for spawning enemies.
public class CombatManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private Transform _enemiesGameObject;

    private void Start()
    {
        // TODO: Get combat participant ids.
        new StartCombatEvent().Fire();
    }
}
