using Entities.Events;
using Events.Common;
using System;
using UnityEngine;

namespace Entities
{
    public class Enemy : MonoBehaviour 
    {
        public GameObject StartingWeapon;
        public int MaxHealth;

        private GameObject _CurrentWeapon;
        private Weapon _weapon;
        private int _currentHealth;
        protected int _instanceId;

        private void Start()
        {
            _currentHealth = MaxHealth;
            _instanceId = gameObject.GetInstanceID();

            SwitchWeapon(StartingWeapon);

            FireInitialEvents();

            RegisterEventListeners();
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
        }

        private void TakeDamage(int damage)
        {
            Debug.Log($"I am taking {damage} damage!");
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
            new EnemyDiedEvent(_instanceId).Fire();
        }

        private void SwitchWeapon(GameObject newWeapon)
        {
            _CurrentWeapon = newWeapon;
            _weapon = _CurrentWeapon.GetComponent<Weapon>();
        }

        // TODO: Pick proper event.
        //private void Attack() => new CharacterAttackedEvent(_weapon.Damage).Fire();

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
        }
    }
}
