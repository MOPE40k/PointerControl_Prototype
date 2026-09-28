using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalMover
{
    public class RigidbodyDirectionalMover : DirectionalMover
    {
        private readonly Rigidbody _rigidbody = null;

        public RigidbodyDirectionalMover(Rigidbody rigidbody, float speed) : base (speed)
            => _rigidbody = rigidbody;

        public override void UpdateTick(float deltaTime)
            => _rigidbody.MovePosition(_rigidbody.position + CurrentVelocity * deltaTime);
    }
}