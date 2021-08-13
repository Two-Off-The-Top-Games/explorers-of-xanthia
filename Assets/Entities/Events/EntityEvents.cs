namespace Events.Entity
{
    public class StartCombatTurnEvent : TargetEvent<StartCombatTurnEvent>
    {
        public StartCombatTurnEvent(int targetInstanceId) : base(targetInstanceId) { }
    }

    public class EntityDiedEvent : SourceEvent<EntityDiedEvent>
    {
        public EntityDiedEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }

    public class EntityTakeDamageEvent : TargetEvent<EntityTakeDamageEvent>
    {
        public EntityTakeDamageEvent(int targetInstanceId, int damage) : base(targetInstanceId)
        {
            Damage = damage;
        }

        public int Damage;
    }

    public class EntityGainHealthEvent : TargetEvent<EntityGainHealthEvent>
    {
        public EntityGainHealthEvent(int targetInstanceId, int health) : base(targetInstanceId)
        {
            Health = health;
        }

        public int Health;
    }

    public class EntityGainMaxHealthEvent : TargetEvent<EntityGainMaxHealthEvent>
    {
        public EntityGainMaxHealthEvent(int targetInstanceId, int maxHealth) : base(targetInstanceId)
        {
            MaxHealth = maxHealth;
        }

        public int MaxHealth;
    }

    public class EnableClickTargetEvent : GlobalEvent<EnableClickTargetEvent> { }

    public class DisableClickTargetEvent : GlobalEvent<DisableClickTargetEvent> { }

    public class ClickTargetSelectedEvent : GlobalEvent<ClickTargetSelectedEvent> 
    {
        public ClickTargetSelectedEvent(int selectedInstanceId)
        {
            SelectedInstanceId = selectedInstanceId;
        }

        public int SelectedInstanceId;
    }
}
