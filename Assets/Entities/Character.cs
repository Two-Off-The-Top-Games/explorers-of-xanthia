using Entities.Events;

namespace Entities
{
    public class Character : Entity
    {
        private int _xp;
        private int _level;

        protected override void Start()
        {
            base.Start();

            _xp = 0;
            _level = 1;

            FireInitialEvents();

            RegisterEventListeners();
        }

        private void FireInitialEvents()
        {
            new CharacterXPChangedEvent(_xp).Fire();
            new CharacterLevelChangedEvent(_level).Fire();
        }

        private void RegisterEventListeners()
        {
            CharacterGainXPEvent.RegisterListener(OnCharacterGainXPEvent);
        }

        private void GainXP(int xp)
        {
            _xp += xp;
            new CharacterXPChangedEvent(_xp).Fire();
            while (_xp >= 100)
            {
                LevelUp();
                _xp -= 100;
                new CharacterXPChangedEvent(_xp).Fire();
            }
        }

        private void LevelUp()
        {
            _level += 1;
            new CharacterLevelChangedEvent(_level).Fire();
        }

        private void OnCharacterGainXPEvent(CharacterGainXPEvent characterGainXPEvent)
        {
            GainXP(characterGainXPEvent.XP);
        }
    }
}
