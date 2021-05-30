using Entities.Events;
using Events.Common;
using GameState.Events;
using System;
using UnityEngine;

namespace Entities
{
    public class Character : MonoBehaviour
    {
        public GameObject StartingWeapon;
        public int MaxHealth;

        private GameObject _CurrentWeapon;
        private Weapon _weapon;
        private int _currentHealth;
        private int _xp;
        private int _level;
        private int _instanceId;

        private void Awake()
        {
            _instanceId = gameObject.GetInstanceID();
            _xp = 0;
            _level = 1;
            _currentHealth = MaxHealth;

            SwitchWeapon(StartingWeapon);

            FireInitialEvents();

            RegisterEventListeners();
        }

        private void FireInitialEvents()
        {
            Debug.Log($"Registering character id: {_instanceId}");
            new RegisterCharacterInstanceIdEvent(_instanceId).Fire();
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth).Fire();
            new CharacterXPChangedEvent(_xp).Fire();
            new CharacterLevelChangedEvent(_level).Fire();
        }

        private void RegisterEventListeners()
        {
            CharacterGainXPEvent.RegisterListener(OnCharacterGainXPEvent);
            CharacterTakeDamageEvent.RegisterListener(OnCharacterTakeDamageEvent);
            CharacterGainHealthEvent.RegisterListener(OnCharacterGainHealthEvent);
            CharacterGainMaxHealthEvent.RegisterListener(OnCharacterGainMaxHealthEvent);
            StartCombatTurnEvent.RegisterListener(_instanceId, OnStartCombatTurnEvent);
            TurnEndedEvent.RegisterListener(_instanceId, OnTurnEndedEvent);
            CharacterAttackEvent.RegisterListener(OnCharacterAttackedEvent);
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

        private void SwitchWeapon(GameObject newWeapon)
        {
            _CurrentWeapon = newWeapon;
            _weapon = _CurrentWeapon.GetComponent<Weapon>();
        }

        private void OnCharacterAttackedEvent(CharacterAttackEvent characterAttackedEvent)
        {
            new EnemyTakeDamageEvent(characterAttackedEvent.EnemyInstanceId, _weapon.Damage).Fire();
        }

        private void OnCharacterTakeDamageEvent(CharacterTakeDamageEvent characterTakeDamageEvent)
        {
            TakeDamage(characterTakeDamageEvent.Damage);
        }

        private void OnCharacterGainHealthEvent(CharacterGainHealthEvent characterGainHealthEvent)
        {
            GainHealth(characterGainHealthEvent.Health);
        }

        private void OnCharacterGainMaxHealthEvent(CharacterGainMaxHealthEvent characterGainMaxHealthEvent)
        {
            GainMaxHealth(characterGainMaxHealthEvent.MaxHealth);
        }
        
        private void OnCharacterGainXPEvent(CharacterGainXPEvent characterGainXPEvent)
        {
            GainXP(characterGainXPEvent.XP);
        }

        private void OnStartCombatTurnEvent(StartCombatTurnEvent eventInfo)
        {
            Debug.Log("Character turn started!");
            new CharacterTurnStartedEvent().Fire();
        }

        private void OnTurnEndedEvent(TurnEndedEvent _)
        {
            Debug.Log("Character turn ended!");
            new CharacterTurnEndedEvent().Fire();
        }
    }
}
