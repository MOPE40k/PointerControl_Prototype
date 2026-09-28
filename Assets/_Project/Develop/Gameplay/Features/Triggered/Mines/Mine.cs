using System.Collections;
using _Project.Develop.Gameplay.Features.HealthFeatures;
using UnityEngine;

namespace _Project.Develop.Gameplay.Features.Triggered.Mines
{
    [RequireComponent(typeof(SphereCollider))]
    public class Mine : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField] private float _radius = 10f;
        [SerializeField] private float _damage = 20f;
        [SerializeField] private float _timeToExplosion = 5f;

        [Space]
        [Header("References:")]
        [SerializeField] private MineView _view = null;

#if UNITY_EDITOR
        [Header("DEBUG SETTINGS:")]
        [SerializeField] private Color _gizmosColor = Color.red;
#endif

        // References
        private Coroutine _timerRoutine = null;

        private void Awake()
        {
            SphereCollider collider = GetComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = _radius;

            OutArea();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out _))
            {
                if (_timerRoutine == null)
                    _timerRoutine = StartCoroutine(TimerRoutine());

                InArea();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out _))
                OutArea();
        }

        private IEnumerator TimerRoutine()
        {
            yield return new WaitForSeconds(_timeToExplosion);

            Explosion();
        }

        private void InArea()
            => _view.InArea();

        private void OutArea()
            => _view.OutArea();

        private void Explosion()
        {
            ApplyEffectToArea();

            _view.Explosion();

            Destroy(gameObject);
        }

        private void ApplyEffectToArea()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            if (colliders.Length == 0)
                return;

            foreach (Collider collider in colliders)
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_damage);
        }

        private void OnDestroy()
        {
            if (_timerRoutine != null)
            {
                StopCoroutine(_timerRoutine);

                _timerRoutine = null;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;

            Gizmos.DrawWireSphere(transform.position, _radius);
        }
#endif
    }
}