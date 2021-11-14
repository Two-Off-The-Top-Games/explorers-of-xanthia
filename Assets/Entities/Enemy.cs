using Entities.Events;
using Events;
using Events.Entity;
using GameState.Events;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Entities
{
    public class Enemy : MonoBehaviour
    {
        public ToggleableSprite CurrentTurnIndicator;
        public GameObject StartingWeapon;
        public int MaxHealth;

        private GameObject _CurrentWeapon;
        private Weapon _weapon;
        private int _currentHealth;
        protected int _instanceId;
        private BoxCollider2D _clickTarget;
        private ActiveCharacters _activeCharacters;
        private ThreadSafeRandom _random;

        private void OnMouseDown()
        {
            new ClickTargetSelectedEvent(_instanceId).Fire();
        }

        private void Awake()
        {
            RequestDependencies();

            _clickTarget = GetComponent<BoxCollider2D>();
            _clickTarget.enabled = false;
            _currentHealth = MaxHealth;
            _instanceId = gameObject.GetInstanceID();

            SwitchWeapon(StartingWeapon);

            FireInitialEvents();

            RegisterEventListeners();
        }

        private void RequestDependencies()
        {
            _activeCharacters = DataSource<ActiveCharacters>.Request();
            _random = DataSource<ThreadSafeRandom>.Request();
        }

        private void FireInitialEvents()
        {
            new EnemyHealthChangedEvent(_instanceId, _currentHealth, MaxHealth).Fire();
        }

        private void RegisterEventListeners()
        {
            EnemyTakeDamageEvent.RegisterListener(_instanceId, OnEnemyTakeDamageEvent);
            EnemyGainHealthEvent.RegisterListener(_instanceId, OnEnemyGainHealthEvent);
            EnemyGainMaxHealthEvent.RegisterListener(_instanceId, OnEnemyGainMaxHealthEvent);
            StartCombatTurnEvent.RegisterListener(_instanceId, OnStartCombatTurnEvent);
            TurnEndedEvent.RegisterListener(_instanceId, OnTurnEndedEvent);
            DisableClickTargetEvent.RegisterListener(OnDisableClickTargetEvent);
            EnableClickTargetEvent.RegisterListener(OnEnableClickTargetEvent);
        }

        private void DeregisterEventListeners()
        {
            EnemyTakeDamageEvent.DeregisterListener(_instanceId);
            EnemyGainHealthEvent.DeregisterListener(_instanceId);
            EnemyGainMaxHealthEvent.DeregisterListener(_instanceId);
            StartCombatTurnEvent.DeregisterListener(_instanceId);
            TurnEndedEvent.DeregisterListener(_instanceId, OnTurnEndedEvent);
            DisableClickTargetEvent.DeregisterListener(OnDisableClickTargetEvent);
            EnableClickTargetEvent.DeregisterListener(OnEnableClickTargetEvent);
        }

        private async Task PerformTurn()
        {
            await Task.Delay(500);
            Attack();
            await Task.Delay(500);
            new EndTurnEvent().Fire();
        }

        private void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            new EnemyHealthChangedEvent(_instanceId, _currentHealth, MaxHealth).Fire();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void GainHealth(int health)
        {
            _currentHealth = Math.Min(_currentHealth + health, MaxHealth);
            new EnemyHealthChangedEvent(_instanceId, _currentHealth, MaxHealth).Fire();
        }

        private void GainMaxHealth(int health)
        {
            MaxHealth += health;
            new EnemyHealthChangedEvent(_instanceId, _currentHealth, MaxHealth).Fire();
            GainHealth(health);
        }

        private void Die()
        {
            DeregisterEventListeners();
            new EntityDiedEvent(_instanceId).Fire();
            new EnemyDiedEvent(_instanceId).Fire();
        }

        private void SwitchWeapon(GameObject newWeapon)
        {
            _CurrentWeapon = newWeapon;
            _weapon = _CurrentWeapon.GetComponent<Weapon>();
        }

        private void Attack()
        {
            int instanceIdToAttack = _activeCharacters.ActiveCharacterIds[_random.Next(0, 2)];
            Debug.Log($"Attacking instance id {instanceIdToAttack}");
            new EntityTakeDamageEvent(instanceIdToAttack, _weapon.Damage).Fire();
        }

        private void OnEnemyTakeDamageEvent(EnemyTakeDamageEvent enemyTakeDamageEvent)
        {
            TakeDamage(enemyTakeDamageEvent.Damage);
        }

        private void OnEnemyGainHealthEvent(EnemyGainHealthEvent enemyGainHealthEvent)
        {
            GainHealth(enemyGainHealthEvent.Health);
        }

        private void OnEnemyGainMaxHealthEvent(EnemyGainMaxHealthEvent enemyGainMaxHealthEvent)
        {
            GainMaxHealth(enemyGainMaxHealthEvent.MaxHealth);
        }

        private void OnStartCombatTurnEvent(StartCombatTurnEvent _)
        {
            Debug.Log("Enemy Turn Started!");
            CurrentTurnIndicator.Enable();

            Task.Run(PerformTurn);
        }

        private void OnTurnEndedEvent(TurnEndedEvent _)
        {
            Debug.Log("Enemy Turn Ended!");
            CurrentTurnIndicator.Disable();
        }

        private void OnEnableClickTargetEvent(EnableClickTargetEvent _)
        {
            _clickTarget.enabled = true;
        }

        private void OnDisableClickTargetEvent(DisableClickTargetEvent _)
        {
            _clickTarget.enabled = false;
        }
    }
}
