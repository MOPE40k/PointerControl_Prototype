using UnityEngine;
namespace _Project.Develop.Gameplay.Features.MovementFeatures
{
    public abstract class RotatorBase : MonoBehaviour
    {
        private enum RotateAxis : byte
        {
            X = 0,
            Y,
            Z
        };

        [Header("Settings:")]
        [SerializeField] private float _speed = 250f;
        [SerializeField] private RotateAxis _rotateAxis = RotateAxis.X;

        protected RotatorBase()
        {
        }

        private void FixedUpdate()
            => Rotate();

        protected Quaternion GetRotateAngleDelta()
            => Quaternion.Euler(GetRotateFrom(_rotateAxis) * _speed * Time.fixedDeltaTime);

        private Vector3 GetRotateFrom(RotateAxis rotateAxis)
            => rotateAxis switch
            {
                RotateAxis.X => Vector3.right,
                RotateAxis.Y => Vector3.up,
                RotateAxis.Z => Vector3.forward,
                _ => throw new System.ArgumentOutOfRangeException(nameof(_rotateAxis), _rotateAxis, "Unknown axis!")
            };

        protected abstract void Rotate();
    }
}