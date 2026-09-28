using _Project.Develop.Gameplay.Features.HealthFeatures;
using UnityEngine;

namespace _Project.Develop.Gameplay.Features.Triggered.FirstAidKit
{
    public class FirstAidKit : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField] private float _healthValue = 10f;

        [Space]
        [Header("References:")]
        [SerializeField] private FirstAidKitView _view = null;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealable healable))
            {
                healable.Heal(_healthValue);

                _view.HealEffect();

                Destroy(gameObject);
            }
        }
    }
}