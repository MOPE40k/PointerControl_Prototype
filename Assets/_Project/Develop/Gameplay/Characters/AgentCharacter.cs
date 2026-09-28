using _Project.Develop.Gameplay.Features.HealthFeatures;
using _Project.Develop.Gameplay.Features.MovementFeatures;
using _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalRotator;
using _Project.Develop.Utils.CoroutineManagment;
using _Project.Develop.Utils.NavMeshManagment;
using UnityEngine;
using UnityEngine.AI;
using TransformDirectionalRotator = _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalRotator.TransformDirectionalRotator;

namespace _Project.Develop.Gameplay.Characters
{
    public class AgentCharacter : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable, IDamageable, IHealable
    {
        // Consts
        private const float DyingStateHealthValue = 0f;

        [Header("Settings:")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 650f;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _timeToSpawn = 3f;

        [Space]
        [Header("References:")]
        [SerializeField] private NavMeshAgent _navMeshAgent = null;
        [SerializeField] private AgentCharacterView _characterView = null;
        [SerializeField] private HealthbarView _healthView = null;
        [SerializeField] private AnimationCurve _jumpCurve = null;

        // References
        private AgentMover _mover = null;
        private AgentJumper _jumper = null;
        private DirectionalRotator _rotator = null;
        private Health _health = null;
        private CoroutineTimer _spawnTimer = null;

        // Runtime
        public bool IsDead { get; private set; } = false;
        public bool IsStoppedMove => _mover.IsStopped;
        public float MoveSpeed => _moveSpeed;
        public Vector3 CurrentVelocity => _mover.CurrentVelocity;
        public Vector3 CurrentDestinationPoint => _mover.CurrentDestination;
        public bool InJumpProcess => _jumper.InProcess;
        public Quaternion CurrentRotation => _rotator.CurrentRotation;
        public Vector3 CurrentPosition => transform.position;
        public float MaxHealth => _health.MaxHealth;
        public float CurrentHealth => _health.CurrentHealth;
        public float TimeToSpawn => _spawnTimer.TimeLimit;

        private void Awake()
        {
            IsDead = false;

            _navMeshAgent.updateRotation = false;

            _mover = new AgentMover(_navMeshAgent, _moveSpeed);
            _jumper = new AgentJumper(_jumpSpeed, _navMeshAgent, this, _jumpCurve);
            _rotator = new TransformDirectionalRotator(this.transform, _rotationSpeed);
            _health = new Health(_maxHealth);
            _spawnTimer = new CoroutineTimer(this);

            StartSpawnTimer();
        }

        private void Update()
            => _rotator.UpdateTick(Time.deltaTime);

        public void StartSpawnTimer()
            => _spawnTimer.StartTimer(_timeToSpawn);

        public bool InSpawnProcess(out float elapsedTime)
            => _spawnTimer.InProcess(out elapsedTime);

        public void ResumeMove()
            => _mover.Resume();

        public void StopMove()
            => _mover.Stop();

        public void SetDestination(Vector3 position)
            => _mover.SetMoveDestination(position);

        public void SetRotateDirection(Vector3 direction)
            => _rotator.SetRotateDirection(direction);

        public void TakeDamage(float damageValue)
        {
            _health.TakeDamage(damageValue);

            if (CurrentHealth <= DyingStateHealthValue)
                IsDead = true;

            _healthView.TakeDamage();

            _characterView.TakeDamage();
        }

        public void Heal(float healthValue)
        {
            _health.Heal(healthValue);

            _healthView.Heal();
        }

        public void Jump(OffMeshLinkData offMeshLinkData)
            => _jumper.Jump(offMeshLinkData);

        public bool TryGetPath(Vector3 targetPosition, NavMeshPath path)
            => NavMeshUtils.TryGetPath(_navMeshAgent, targetPosition, path);

        public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
        {
            if (_navMeshAgent.isOnOffMeshLink)
            {
                offMeshLinkData = _navMeshAgent.currentOffMeshLinkData;

                return true;
            }

            offMeshLinkData = default;

            return false;
        }

        public void DestroyCharacter()
            => Destroy(gameObject);
    }
}