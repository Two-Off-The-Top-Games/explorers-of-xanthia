namespace Events.Common
{
    public class StartCombatTurnEvent : TargetEvent<StartCombatTurnEvent>
    {
        public StartCombatTurnEvent(int targetInstanceId) : base(targetInstanceId) { }
    }

    public class EntityDiedEvent : SourceEvent<EntityDiedEvent>
    {
        public EntityDiedEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }
}
