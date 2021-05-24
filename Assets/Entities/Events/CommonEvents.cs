namespace Events.Common
{
    public class StartCombatTurnEvent : TargetEvent<StartCombatTurnEvent>
    {
        public StartCombatTurnEvent(int targetInstanceId) : base(targetInstanceId) { }
    }
}
