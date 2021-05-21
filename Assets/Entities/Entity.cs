using UnityEngine;
using System;
using Entities.Events;

namespace Entities {
    public abstract class Entity : MonoBehaviour
    {
        public GameObject StartingWeapon;
        private GameObject _CurrentWeapon;
        private Weapon _weapon;
        private int _currentHealth;
        public int MaxHealth;

        protected virtual void Start()
        {
            _currentHealth = MaxHealth;
            SwitchWeapon(StartingWeapon);

            FireInitialEvents();

            RegisterEventListeners();
        }

        private void FireInitialEvents()
        {
            new EntityHealthChangedEvent(_currentHealth, MaxHealth).Fire();
        }

        private void RegisterEventListeners()
        {
            EntityTakeDamageEvent.RegisterListener(OnEntityTakeDamageEvent);
            EntityGainHealthEvent.RegisterListener(OnEntityGainHealthEvent);
            EntityGainMaxHealthEvent.RegisterListener(OnEntityGainMaxHealthEvent);
        }

        private void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            new EntityHealthChangedEvent(_currentHealth, MaxHealth).Fire();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void GainHealth(int health)
        {
            _currentHealth = Math.Min(_currentHealth + health, MaxHealth);
            new EntityHealthChangedEvent(_currentHealth, MaxHealth).Fire();
        }

        private void GainMaxHealth(int health)
        {
            MaxHealth += health;
            new EntityHealthChangedEvent(_currentHealth, MaxHealth).Fire();
            GainHealth(health);
        }

        private void Die()
        {
            new EntityDiedEvent().Fire();
        }

        private void SwitchWeapon(GameObject newWeapon)
        {
            _CurrentWeapon = newWeapon;
            _weapon = _CurrentWeapon.GetComponent<Weapon>();
        }

        // TODO: Pick proper event.
        //private void Attack() => new CharacterAttackedEvent(_weapon.Damage).Fire();

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
    }
}
