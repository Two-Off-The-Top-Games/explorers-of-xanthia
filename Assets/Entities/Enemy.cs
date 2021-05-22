using Entities.Events;
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
            _instanceId = GetInstanceID();

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
            EnemyTakeDamageEvent.RegisterListener(_instanceId, OnEntityTakeDamageEvent);
            EnemyGainHealthEvent.RegisterListener(_instanceId, OnEntityGainHealthEvent);
            EnemyGainMaxHealthEvent.RegisterListener(_instanceId, OnEntityGainMaxHealthEvent);
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

        // TODO: Pick proper event.
        //private void Attack() => new CharacterAttackedEvent(_weapon.Damage).Fire();

        private void OnEntityTakeDamageEvent(EnemyTakeDamageEvent entityTakeDamageEvent)
        {
            TakeDamage(entityTakeDamageEvent.Damage);
        }

        private void OnEntityGainHealthEvent(EnemyGainHealthEvent entityGainHealthEvent)
        {
            GainHealth(entityGainHealthEvent.Health);
        }

        private void OnEntityGainMaxHealthEvent(EnemyGainMaxHealthEvent entityGainMaxHealthEvent)
        {
            GainMaxHealth(entityGainMaxHealthEvent.MaxHealth);
        }
    }
}
