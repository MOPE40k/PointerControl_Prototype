using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalRotator
{
    public abstract class DirectionalRotator
    {
        // Consts
        private const float DistanceTreshold = 0.05f;

        // Settings
        private readonly float _speed = 0f;

        // Runtime
        private Vector3 _currentDirection = Vector3.zero;

        public DirectionalRotator(float speed)
            => _speed = speed;

        // Runtime
        public abstract Quaternion CurrentRotation { get; }

        public void SetRotateDirection(Vector3 direction)
            => _currentDirection = direction;

        public void UpdateTick(float deltaTime)
        {
            if (IsCloseEnough())
                return;

            Quaternion targetRotation = Quaternion.LookRotation(_currentDirection);

            float deltaRotation = _speed * deltaTime;

            ApplyRotation(Quaternion.RotateTowards(CurrentRotation, targetRotation, deltaRotation));
        }

        protected abstract void ApplyRotation(Quaternion rotation);

        private bool IsCloseEnough()
            => _currentDirection.sqrMagnitude < DistanceTreshold * DistanceTreshold;
    }
}