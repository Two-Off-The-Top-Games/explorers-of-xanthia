using Entities.Events;
using Events.Common;
using GameState.Events;
using System;
using System.Threading.Tasks;
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

        private void Awake()
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
            new EnemyDiedEvent(_instanceId).Fire();
        }

        private void SwitchWeapon(GameObject newWeapon)
        {
            _CurrentWeapon = newWeapon;
            _weapon = _CurrentWeapon.GetComponent<Weapon>();
        }

        private void Attack()
        {
            new CharacterTakeDamageEvent(_weapon.Damage).Fire();
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

            Task.Run(PerformTurn);
        }
    }
}
