using Events;

namespace Character.Events
{
    public class CharacterDiedEvent : Event<CharacterDiedEvent> { }

    public class CharacterHealthChangedEvent : Event<CharacterHealthChangedEvent>
    {
        public CharacterHealthChangedEvent(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public int CurrentHealth;
        public int MaxHealth;
    }

    public class CharacterXPChangedEvent : Event<CharacterXPChangedEvent>
    {
        public CharacterXPChangedEvent(int xp)
        {
            XP = xp;
        }

        public int XP;
    }

    public class CharacterLevelChangedEvent : Event<CharacterLevelChangedEvent>
    {
        public CharacterLevelChangedEvent(int level)
        {
            Level = level;
        }

        public int Level;
    }

    public class CharacterAttackedEvent : Event<CharacterAttackedEvent>
    {
        public CharacterAttackedEvent(int damage)
        {
            Damage = damage;
        }

        public int Damage;
    }
}