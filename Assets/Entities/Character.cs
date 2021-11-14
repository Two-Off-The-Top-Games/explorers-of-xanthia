using Entities.Events;
using Events.Entity;
using GameState.Events;
using System;
using UnityEngine;

namespace Entities
{
    public class Character : MonoBehaviour
    {
        public ToggleableSprite Indicator;
        public GameObject StartingWeapon;
        public int MaxHealth;
        public int ActionPoints;

        private bool _isMyTurn;
        private GameObject _CurrentWeapon;
        private Weapon _weapon;
        private int _currentHealth;
        private int _xp;
        private int _level;
        private int _currentActionPoints;
        private int _instanceId;
        private BoxCollider2D _clickTarget;

        private void Awake()
        {
            _instanceId = gameObject.GetInstanceID();
            _clickTarget = GetComponent<BoxCollider2D>();
            _clickTarget.enabled = false;
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
            new CharacterHealthChangedEvent(_instanceId, _currentHealth, _currentHealth, MaxHealth, MaxHealth).Fire();
            new CharacterXPChangedEvent(_instanceId, _xp).Fire();
            new CharacterLevelChangedEvent(_instanceId, _level).Fire();
            new CharacterActionPointsChangedEvent(_instanceId, _currentActionPoints, ActionPoints).Fire();
        }

        private void RegisterEventListeners()
        {
            CharacterGainXPEvent.RegisterListener(_instanceId, OnCharacterGainXPEvent);
            EntityTakeDamageEvent.RegisterListener(_instanceId, OnEntityTakeDamageEvent);
            EntityGainHealthEvent.RegisterListener(_instanceId, OnEntityGainHealthEvent);
            EntityGainMaxHealthEvent.RegisterListener(_instanceId, OnEntityGainMaxHealthEvent);
            StartCombatTurnEvent.RegisterListener(_instanceId, OnStartCombatTurnEvent);
            TurnEndedEvent.RegisterListener(_instanceId, OnTurnEndedEvent);
            CharacterAttackEvent.RegisterListener(_instanceId, OnCharacterAttackEvent);
            EnableClickTargetEvent.RegisterListener(OnEnableClickTargetEvent);
            DisableClickTargetEvent.RegisterListener(OnDisableClickTargetEvent);
        }

        private void OnMouseDown()
        {
            new ClickTargetSelectedEvent(_instanceId).Fire();
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

        public void TakeDamage(int damage)
        {
            int previousHealth = _currentHealth;
            _currentHealth -= damage;
            new CharacterHealthChangedEvent(_instanceId, previousHealth, _currentHealth, MaxHealth, MaxHealth).Fire();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void GainHealth(int health)
        {
            int previousHealth = _currentHealth;
            _currentHealth = Math.Min(_currentHealth + health, MaxHealth);
            new CharacterHealthChangedEvent(_instanceId, previousHealth, _currentHealth, MaxHealth, MaxHealth).Fire();
        }

        public void GainMaxHealth(int health)
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
            ClickTargetSelectedEvent.RegisterListener(OnClickTargetSelectedEvent);
            new EnableClickTargetEvent().Fire();
            new CharacterStartedAttackEvent(_instanceId).Fire();
        }

        private void OnEntityTakeDamageEvent(EntityTakeDamageEvent entityTakeDamageEvent)
        {
            TakeDamage(entityTakeDamageEvent.Damage);
        }

        private void OnEntityGainHealthEvent(EntityGainHealthEvent entityGainHealthEvent)
        {
            GainHealth(entityGainHealthEvent.Health);
        }

        private void OnEntityGainMaxHealthEvent(EntityGainMaxHealthEvent entityGainMaxHealthEvent)
        {
            GainMaxHealth(entityGainMaxHealthEvent.MaxHealth);
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
            _isMyTurn = true;
            Indicator.Enable();
        }

        private void OnTurnEndedEvent(TurnEndedEvent _)
        {
            Debug.Log("Character turn ended!");
            new CharacterTurnEndedEvent(_instanceId).Fire();
            _isMyTurn = false;
            Indicator.Disable();
        }

        private void OnEnableClickTargetEvent(EnableClickTargetEvent _)
        {
            _clickTarget.enabled = true;
            Indicator.Enable();
        }

        private void OnDisableClickTargetEvent(DisableClickTargetEvent _)
        {
            _clickTarget.enabled = false;
            Indicator.Toggle(_isMyTurn);
        }

        private void OnClickTargetSelectedEvent(ClickTargetSelectedEvent clickTargetSelectedEvent)
        {
            new EntityTakeDamageEvent(clickTargetSelectedEvent.SelectedInstanceId, _weapon.Damage).Fire();
            new DisableClickTargetEvent().Fire();
            new CharacterFinishedAttackEvent(_instanceId).Fire();
            ClickTargetSelectedEvent.DeregisterListener(OnClickTargetSelectedEvent);
        }
    }
}
