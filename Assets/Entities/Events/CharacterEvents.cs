using Events;

namespace Entities.Events
{
    public class CharacterGainXPEvent : Event<CharacterGainXPEvent>
    {
        public CharacterGainXPEvent(int xp)
        {
            XP = xp;
        }

        public int XP;
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
}