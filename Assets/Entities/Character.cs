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

        private Inventory _inventory;
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

            _inventory = new Inventory();

            SwitchWeapon(StartingWeapon);

            FireInitialEvents();

            RegisterEventListeners();
        }

        private void FireInitialEvents()
        {
            new CharacterHealthChangedEvent(_instanceId, _currentHealth, _currentHealth, MaxHealth, MaxHealth).Fire();
            new CharacterXPChangedEvent(_instanceId, _xp).Fire();
            new CharacterLevelChangedEvent(_instanceId, _level).Fire();
            new CharacterActionPointsChangedEvent(_instanceId, _currentActionPoints, ActionPoints).Fire();
        }

        private void RegisterEventListeners()
        {
            CharacterGainXPEvent.RegisterListener(_instanceId, OnCharacterGainXPEvent);
            CharacterTakeDamageEvent.RegisterListener(_instanceId, OnCharacterTakeDamageEvent);
            CharacterGainHealthEvent.RegisterListener(_instanceId, OnCharacterGainHealthEvent);
            CharacterGainMaxHealthEvent.RegisterListener(_instanceId, OnCharacterGainMaxHealthEvent);
            StartCombatTurnEvent.RegisterListener(_instanceId, OnStartCombatTurnEvent);
            TurnEndedEvent.RegisterListener(_instanceId, OnTurnEndedEvent);
            CharacterAttackEvent.RegisterListener(_instanceId, OnCharacterAttackEvent);
            CharacterSelectedAttackTargetEvent.RegisterListener(_instanceId, OnCharacterSelectedAttackTargetEvent);
        }

        private void GainXP(int xp)
        {
            _xp += xp;
            new CharacterXPChangedEvent(_instanceId, _xp).Fire();
            while (_xp >= 100)
            {
                LevelUp();
                _xp -= 100;
                new CharacterXPChangedEvent(_instanceId, _xp).Fire();
            }
        }

        private void LevelUp()
        {
            _level += 1;
            new CharacterLevelChangedEvent(_instanceId, _level).Fire();
        }

        private void TakeDamage(int damage)
        {
            int previousHealth = _currentHealth;
            _currentHealth -= damage;
            new CharacterHealthChangedEvent(_instanceId, previousHealth, _currentHealth, MaxHealth, MaxHealth).Fire();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void GainHealth(int health)
        {
            int previousHealth = _currentHealth;
            _currentHealth = Math.Min(_currentHealth + health, MaxHealth);
            new CharacterHealthChangedEvent(_instanceId, previousHealth, _currentHealth, MaxHealth, MaxHealth).Fire();
        }

        private void GainMaxHealth(int health)
        {
            int previousMaxHealth = MaxHealth;
            MaxHealth += health;
            new CharacterHealthChangedEvent(_instanceId, _currentHealth, _currentHealth, previousMaxHealth, MaxHealth).Fire();
            GainHealth(health);
        }

        private void Die()
        {
            new EntityDiedEvent(_instanceId).Fire();
            new CharacterDiedEvent(_instanceId).Fire();
        }

        private void SwitchWeapon(GameObject newWeapon)
        {
            _CurrentWeapon = newWeapon;
            _weapon = _CurrentWeapon.GetComponent<Weapon>();
        }

        private void OnCharacterAttackEvent(CharacterAttackEvent characterAttackedEvent)
        {
            new CharacterStartedAttackEvent(_instanceId).Fire();
            new EnableAttackTargetsEvent(_instanceId, _weapon.Damage).Fire();
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
            new CharacterActionPointsChangedEvent(_instanceId, _currentActionPoints, ActionPoints).Fire();
            new CharacterTurnStartedEvent(_instanceId).Fire();
        }

        private void OnTurnEndedEvent(TurnEndedEvent _)
        {
            Debug.Log("Character turn ended!");
            new CharacterTurnEndedEvent(_instanceId).Fire();
        }

        private void OnCharacterSelectedAttackTargetEvent(CharacterSelectedAttackTargetEvent _)
        {
            new CharacterFinishedAttackEvent(_instanceId).Fire();
            _currentActionPoints -= 1;
            new CharacterActionPointsChangedEvent(_instanceId, _currentActionPoints, ActionPoints).Fire();
        }
    }
}
