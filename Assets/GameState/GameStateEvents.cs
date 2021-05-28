namespace Events.GameState
{
    public class EndTurnEvent : GlobalEvent<EndTurnEvent> { }

    public class StartCombatEvent : GlobalEvent<StartCombatEvent>
    {
        public StartCombatEvent(params int[] orderedParticipantIds)
        {
            OrderedParticipantIds = orderedParticipantIds;
        }

        public int[] OrderedParticipantIds;
    }

    public class EndCombatEvent : GlobalEvent<EndCombatEvent> { }

    public class TurnEndedEvent : SourceEvent<TurnEndedEvent>
    {
        public TurnEndedEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }
}
