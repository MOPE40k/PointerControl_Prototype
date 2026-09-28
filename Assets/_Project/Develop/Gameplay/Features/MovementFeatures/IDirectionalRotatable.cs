using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures
{
    public interface IDirectionalRotatable
    {
        Quaternion CurrentRotation { get; }

        void SetRotateDirection(Vector3 direction);
    }
}