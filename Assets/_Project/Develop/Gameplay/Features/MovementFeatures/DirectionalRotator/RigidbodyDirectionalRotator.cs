using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalRotator
{
    public class RigidbodyDirectionalRotator : DirectionalRotator
    {
        // References
        private readonly Rigidbody _rigidbody = null;

        public RigidbodyDirectionalRotator(Rigidbody rigidbody, float speed) : base (speed)
            => _rigidbody = rigidbody;

        public override Quaternion CurrentRotation => _rigidbody.rotation;

        protected override void ApplyRotation(Quaternion rotation)
            => _rigidbody.rotation = rotation;
    }
}