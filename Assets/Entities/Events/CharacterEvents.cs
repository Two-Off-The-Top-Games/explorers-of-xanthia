using Events;

namespace Entities.Events
{
    public class RegisterCharacterInstanceIdEvent : GlobalEvent<RegisterCharacterInstanceIdEvent>
    {
        public RegisterCharacterInstanceIdEvent(int characterInstanceId)
        {
            CharacterInstanceId = characterInstanceId;
        }

        public int CharacterInstanceId;
    }

    public class CharacterGainXPEvent : GlobalEvent<CharacterGainXPEvent>
    {
        public CharacterGainXPEvent(int xp)
        {
            XP = xp;
        }

        public int XP;
    }

    public class CharacterXPChangedEvent : GlobalEvent<CharacterXPChangedEvent>
    {
        public CharacterXPChangedEvent(int xp)
        {
            XP = xp;
        }

        public int XP;
    }

    public class CharacterLevelChangedEvent : GlobalEvent<CharacterLevelChangedEvent>
    {
        public CharacterLevelChangedEvent(int level)
        {
            Level = level;
        }

        public int Level;
    }

    public class CharacterDiedEvent : GlobalEvent<CharacterDiedEvent>
    {
        public CharacterDiedEvent() { }
    }

    public class CharacterHealthChangedEvent : GlobalEvent<CharacterHealthChangedEvent>
    {
        public CharacterHealthChangedEvent(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public int CurrentHealth;
        public int MaxHealth;
    }

    public class CharacterTakeDamageEvent : GlobalEvent<CharacterTakeDamageEvent>
    {
        public CharacterTakeDamageEvent(int damage)
        {
            Damage = damage;
        }

        public int Damage;
    }

    public class CharacterGainHealthEvent : GlobalEvent<CharacterGainHealthEvent>
    {
        public CharacterGainHealthEvent(int health)
        {
            Health = health;
        }

        public int Health;
    }

    public class CharacterGainMaxHealthEvent : GlobalEvent<CharacterGainMaxHealthEvent>
    {
        public CharacterGainMaxHealthEvent(int maxHealth)
        {
            MaxHealth = maxHealth;
        }

        public int MaxHealth;
    }

    public class CharacterTurnStartedEvent : GlobalEvent<CharacterTurnStartedEvent> { }

    public class CharacterTurnEndedEvent : GlobalEvent<CharacterTurnEndedEvent> { }
}