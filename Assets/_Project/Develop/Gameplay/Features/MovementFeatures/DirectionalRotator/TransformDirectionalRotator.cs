using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalRotator
{
    public class TransformDirectionalRotator : DirectionalRotator
    {
        // References
        private readonly Transform _transform = null;

        public TransformDirectionalRotator(Transform transform, float speed) : base (speed)
            => _transform = transform;

        public override Quaternion CurrentRotation => _transform.rotation;

        protected override void ApplyRotation(Quaternion rotation)
            => _transform.rotation = rotation;
    }
}