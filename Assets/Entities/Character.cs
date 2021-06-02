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
        public int ActionPoints;

        private GameObject _CurrentWeapon;
        private Weapon _weapon;
        private int _currentHealth;
        private int _xp;
        private int _level;
        private int _currentActionPoints;
        private int _instanceId;

        private void Awake()
        {
            _instanceId = gameObject.GetInstanceID();
            _xp = 0;
            _level = 1;
            _currentHealth = MaxHealth;
            _currentActionPoints = ActionPoints;

            SwitchWeapon(StartingWeapon);

            FireInitialEvents();

            RegisterEventListeners();
        }

        private void FireInitialEvents()
        {
            new RegisterCharacterInstanceIdEvent(_instanceId).Fire();
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth, 0).Fire();
            new CharacterXPChangedEvent(_xp).Fire();
            new CharacterLevelChangedEvent(_level).Fire();
            new CharacterActionPointsChangedEvent(_currentActionPoints, ActionPoints).Fire();
        }

        private void RegisterEventListeners()
        {
            CharacterGainXPEvent.RegisterListener(OnCharacterGainXPEvent);
            CharacterTakeDamageEvent.RegisterListener(OnCharacterTakeDamageEvent);
            CharacterGainHealthEvent.RegisterListener(OnCharacterGainHealthEvent);
            CharacterGainMaxHealthEvent.RegisterListener(OnCharacterGainMaxHealthEvent);
            StartCombatTurnEvent.RegisterListener(_instanceId, OnStartCombatTurnEvent);
            TurnEndedEvent.RegisterListener(_instanceId, OnTurnEndedEvent);
            CharacterAttackEvent.RegisterListener(OnCharacterAttackEvent);
            CharacterSelectedAttackTargetEvent.RegisterListener(OnCharacterSelectedAttackTargetEvent);
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
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth, -damage).Fire();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void GainHealth(int health)
        {
            _currentHealth = Math.Min(_currentHealth + health, MaxHealth);
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth, health).Fire();
        }

        private void GainMaxHealth(int health)
        {
            MaxHealth += health;
            new CharacterHealthChangedEvent(_currentHealth, MaxHealth, 0).Fire();
            GainHealth(health);
        }

        private void Die()
        {
            new EntityDiedEvent(_instanceId).Fire();
            new CharacterDiedEvent().Fire();
        }

        private void SwitchWeapon(GameObject newWeapon)
        {
            _CurrentWeapon = newWeapon;
            _weapon = _CurrentWeapon.GetComponent<Weapon>();
        }

        private void OnCharacterAttackEvent(CharacterAttackEvent characterAttackedEvent)
        {
            new EnableAttackTargetsEvent(_weapon.Damage).Fire();
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
            _currentActionPoints = ActionPoints;
            new CharacterActionPointsChangedEvent(_currentActionPoints, ActionPoints).Fire();
            new CharacterTurnStartedEvent().Fire();
        }

        private void OnTurnEndedEvent(TurnEndedEvent _)
        {
            Debug.Log("Character turn ended!");
            new CharacterTurnEndedEvent().Fire();
        }

        private void OnCharacterSelectedAttackTargetEvent(CharacterSelectedAttackTargetEvent _)
        {
            _currentActionPoints -= 1;
            new CharacterActionPointsChangedEvent(_currentActionPoints, ActionPoints).Fire();
        }
    }
}
