using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures
{

    public sealed class TransformRotator : RotatorBase
    {
        [Header("References:")]
        [SerializeField] private Transform _rotatable = null;

        private void Awake()
        {
            if (_rotatable == null)
                _rotatable = this.transform;
        }

        protected override void Rotate()
            => _rotatable.rotation *= GetRotateAngleDelta();
    }
}