using Events.Common;
using GameState.Events;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private Queue<int> _turnOrder = new Queue<int>();
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
        _combatIsActive = true;
        StartNextTurn();
    }

    private void OnEndTurnEvent(EndTurnEvent _)
    {
        AdvanceTurn();
    }

    private void StartNextTurn()
    {
        int instanceId = _turnOrder.Peek();
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
}
