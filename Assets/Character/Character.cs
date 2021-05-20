using UnityEngine;
using Character.Events;
using System;

namespace Character {
    public class Character : MonoBehaviour
    {
        private int _xp;
        private int _level;
        private int _currentHealth;
        public int MaxHealth;

        private void Start()
        {
            _xp = 0;
            _level = 1;
            _currentHealth = MaxHealth;

            FireInitialEvents();

            RegisterEventListeners();
        }

        private void FireInitialEvents()
        {
            new CharacterXPChangedEvent(_xp).Fire();
            new CharacterLevelChangedEvent(_level).Fire();
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth).Fire();
        }

        private void RegisterEventListeners()
        {
            CharacterAttackedEvent.RegisterListener(OnCharacterAttackedEvent);
            CharacterGainXPEvent.RegisterListener(OnCharacterGainXPEvent);
            CharacterGainHealthEvent.RegisterListener(OnCharacterGainHealthEvent);
            CharacterGainMaxHealthEvent.RegisterListener(OnCharacterGainMaxHealthEvent);
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

        private void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth).Fire();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void GainHealth(int health)
        {
            _currentHealth = Math.Min(_currentHealth + health, MaxHealth);
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth).Fire();
        }

        private void GainMaxHealth(int health)
        {
            MaxHealth += health;
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth).Fire();
            GainHealth(health);
        }

        private void Die()
        {
            new CharacterDiedEvent().Fire();
        }

        private void OnCharacterAttackedEvent(CharacterAttackedEvent characterAttackedEvent)
        {
            TakeDamage(characterAttackedEvent.Damage);
        }

        private void OnCharacterGainXPEvent(CharacterGainXPEvent characterGainXPEvent)
        {
            GainXP(characterGainXPEvent.XP);
        }

        private void OnCharacterGainHealthEvent(CharacterGainHealthEvent characterGainHealthEvent)
        {
            GainHealth(characterGainHealthEvent.Health);
        }

        private void OnCharacterGainMaxHealthEvent(CharacterGainMaxHealthEvent characterGainMaxHealthEvent)
        {
            GainMaxHealth(characterGainMaxHealthEvent.MaxHealth);
        }
    }
}
