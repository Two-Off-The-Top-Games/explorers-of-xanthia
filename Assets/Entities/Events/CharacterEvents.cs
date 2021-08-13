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

    public class CharacterGainXPEvent : TargetEvent<CharacterGainXPEvent>
    {
        public CharacterGainXPEvent(int targetInstanceId, int xp) : base(targetInstanceId)
        {
            XP = xp;
        }

        public int XP;
    }

    public class CharacterXPChangedEvent : SourceEvent<CharacterXPChangedEvent>
    {
        public CharacterXPChangedEvent(int sourceInstanceId, int xp) : base(sourceInstanceId)
        {
            XP = xp;
        }

        public int XP;
    }

    public class CharacterLevelChangedEvent : SourceEvent<CharacterLevelChangedEvent>
    {
        public CharacterLevelChangedEvent(int sourceInstanceId, int level) : base(sourceInstanceId)
        {
            Level = level;
        }

        public int Level;
    }

    public class CharacterDiedEvent : SourceEvent<CharacterDiedEvent>
    {
        public CharacterDiedEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }

    public class CharacterHealthChangedEvent : SourceEvent<CharacterHealthChangedEvent>
    {
        public CharacterHealthChangedEvent(int sourceInstanceId, int previousHealth, int currentHealth, int previousMaxHealth, int maxHealth) : base(sourceInstanceId)
        {
            PreviousHealth = previousHealth;
            CurrentHealth = currentHealth;
            PreviousMaxHealth = previousMaxHealth;
            MaxHealth = maxHealth;
        }

        public int PreviousHealth;
        public int CurrentHealth;
        public int PreviousMaxHealth;
        public int MaxHealth;
    }

    public class CharacterActionPointsChangedEvent : SourceEvent<CharacterActionPointsChangedEvent>
    {
        public CharacterActionPointsChangedEvent(int sourceInstanceId, int currentActionPoints, int maxActionPoints) : base(sourceInstanceId)
        {
            CurrentActionPoints = currentActionPoints;
            MaxActionPoints = maxActionPoints;
        }

        public int CurrentActionPoints;
        public int MaxActionPoints;
    }

    public class CharacterTurnStartedEvent : SourceEvent<CharacterTurnStartedEvent> 
    {
        public CharacterTurnStartedEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }

    public class CharacterTurnEndedEvent : SourceEvent<CharacterTurnEndedEvent>
    {
        public CharacterTurnEndedEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }

    public class CharacterAttackEvent : TargetEvent<CharacterAttackEvent> 
    {
        public CharacterAttackEvent(int targetInstanceId) : base(targetInstanceId) { }
    }

    public class CharacterSelectedAttackTargetEvent : TargetEvent<CharacterSelectedAttackTargetEvent> 
    {
        public CharacterSelectedAttackTargetEvent(int targetInstanceId) : base(targetInstanceId) { }
    }

    public class CharacterStartedAttackEvent : SourceEvent<CharacterStartedAttackEvent>
    {
        public CharacterStartedAttackEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }

    public class CharacterFinishedAttackEvent : SourceEvent<CharacterFinishedAttackEvent>
    {
        public CharacterFinishedAttackEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }
}