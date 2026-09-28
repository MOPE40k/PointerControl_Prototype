using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalMover
{
    public class TransformDirectionalMover : DirectionalMover
    {
        private readonly Transform _transform = null;

        public TransformDirectionalMover(Transform transform, float speed) : base (speed)
            => _transform = transform;

        public override void UpdateTick(float deltaTime)
            => _transform.position += CurrentVelocity;
    }
}