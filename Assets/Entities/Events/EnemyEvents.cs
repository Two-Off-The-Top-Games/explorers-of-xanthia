using Events;

namespace Entities.Events
{
    public class EnemyDiedEvent : SourceEvent<EnemyDiedEvent>
    {
        public EnemyDiedEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }

    public class EnemyHealthChangedEvent : SourceEvent<EnemyHealthChangedEvent>
    {
        public EnemyHealthChangedEvent(int sourceInstanceId, int currentHealth, int maxHealth) : base(sourceInstanceId)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public int CurrentHealth;
        public int MaxHealth;
    }

    public class EnemyTakeDamageEvent : TargetEvent<EnemyTakeDamageEvent>
    {
        public EnemyTakeDamageEvent(int targetInstanceId, int damage) : base(targetInstanceId)
        {
            Damage = damage;
        }

        public int Damage;
    }

    public class EnemyGainHealthEvent : TargetEvent<EnemyGainHealthEvent>
    {
        public EnemyGainHealthEvent(int targetInstanceId, int health) : base(targetInstanceId)
        {
            Health = health;
        }

        public int Health;
    }

    public class EnemyGainMaxHealthEvent : TargetEvent<EnemyGainMaxHealthEvent>
    {
        public EnemyGainMaxHealthEvent(int targetInstanceId, int maxHealth) : base(targetInstanceId)
        {
            MaxHealth = maxHealth;
        }

        public int MaxHealth;
    }
}