using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalMover
{
    public abstract class DirectionalMover
    {
        // Settings
        private readonly float _speed = 0f;

        // Runtime
        private Vector3 _currentDirection = Vector3.zero;

        public DirectionalMover(float speed)
            => _speed = speed;

        // Runtime
        protected Vector3 CurrentVelocity => _currentDirection.normalized * _speed;

        public void SetDirection(Vector3 direction)
            => _currentDirection = direction;
            
        public abstract void UpdateTick(float deltaTime);
    }
}