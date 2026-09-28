using UnityEngine;

namespace _Project.Develop.Gameplay.Features.HealthFeatures
{
    public class Health
    {
        // Settings
        private readonly float _maxHealth = 0f;

        // Runtime
        private float _currentHealth = 0f;

        public Health(float maxHealth)
        {
            _maxHealth = maxHealth;

            _currentHealth = _maxHealth;
        }

        // Runtime
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        public void Heal(float healthValue)
        {
            if (healthValue <= 0)
            {
                Debug.LogWarning("The health value cannot be less than or equal to zero");

                return;
            }

            _currentHealth = Mathf.Min(_currentHealth + healthValue, _maxHealth);
        }

        public void TakeDamage(float damageValue)
        {
            if (damageValue <= 0)
            {
                Debug.LogWarning("The damage value cannot be less than or equal to zero");

                return;
            }

            _currentHealth = Mathf.Max(0, _currentHealth - damageValue);
        }
    }
}