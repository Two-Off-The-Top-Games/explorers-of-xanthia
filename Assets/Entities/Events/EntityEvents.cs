using Events;

namespace Entities.Events
{
    public class EntityDiedEvent : Event<EntityDiedEvent> { }

    public class EntityHealthChangedEvent : Event<EntityHealthChangedEvent>
    {
        public EntityHealthChangedEvent(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public int CurrentHealth;
        public int MaxHealth;
    }

    public class EntityTakeDamageEvent : Event<EntityTakeDamageEvent>
    {
        public EntityTakeDamageEvent(int damage)
        {
            Damage = damage;
        }

        public int Damage;
    }

    public class EntityGainHealthEvent : Event<EntityGainHealthEvent>
    {
        public EntityGainHealthEvent(int health)
        {
            Health = health;
        }

        public int Health;
    }

    public class EntityGainMaxHealthEvent : Event<EntityGainMaxHealthEvent>
    {
        public EntityGainMaxHealthEvent(int maxHealth)
        {
            MaxHealth = maxHealth;
        }

        public int MaxHealth;
    }
}