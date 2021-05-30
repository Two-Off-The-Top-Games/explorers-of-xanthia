using Events.Common;
using GameState.Events;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private Queue<int> _turnOrder = new Queue<int>();
    private HashSet<int> _deadEntities = new HashSet<int>();
    private bool _combatIsActive = false;

    private void OnEnable()
    {
        StartCombatEvent.RegisterListener(OnStartCombatEvent);
        EndTurnEvent.RegisterListener(OnEndTurnEvent);
        EndCombatEvent.RegisterListener(OnEndCombatEvent);
    }

    private void OnDisable()
    {
        StartCombatEvent.DeregisterListener(OnStartCombatEvent);
        EndTurnEvent.DeregisterListener(OnEndTurnEvent);
        EndCombatEvent.DeregisterListener(OnEndCombatEvent);
    }

    private void OnStartCombatEvent(StartCombatEvent startCombatEvent)
    {
        StartCombat(startCombatEvent.OrderedParticipantIds);
    }

    private void StartCombat(int[] orderedParticipantIds)
    {
        _turnOrder = new Queue<int>(orderedParticipantIds);
        for (int i = 0; i < orderedParticipantIds.Length; i++)
        {
            EntityDiedEvent.RegisterListener(orderedParticipantIds[i], OnEntityDiedEvent);
        }
        _combatIsActive = true;
        StartNextTurn();
    }

    private void OnEndTurnEvent(EndTurnEvent _)
    {
        AdvanceTurn();
    }

    private void StartNextTurn()
    {
        int instanceId;
        while (true)
        {
            instanceId = _turnOrder.Peek();
            // If the entity isn't dead, we can just start their turn like normal.
            if (!_deadEntities.Contains(instanceId))
            {
                break;
            }
            // If they are dead, we need to remove them and dequeue them.
            _deadEntities.Remove(instanceId);
            _turnOrder.Dequeue();
        }
        new StartCombatTurnEvent(instanceId).Fire();
    }

    private void AdvanceTurn()
    {
        int instanceId = _turnOrder.Dequeue();
        new TurnEndedEvent(instanceId).Fire();
        _turnOrder.Enqueue(instanceId);
        StartNextTurn();
    }

    private void OnEndCombatEvent(EndCombatEvent _)
    {
        EndCombat();
    }

    private void EndCombat()
    {
        _combatIsActive = false;
        // Realistically don't need to do this, but could save some memory.
        _turnOrder = null;
    }

    private void OnEntityDiedEvent(EntityDiedEvent entityDiedEvent)
    {
        _deadEntities.Add(entityDiedEvent.SourceInstanceId);
        EntityDiedEvent.DeregisterListener(entityDiedEvent.SourceInstanceId, OnEntityDiedEvent);
    }
}
