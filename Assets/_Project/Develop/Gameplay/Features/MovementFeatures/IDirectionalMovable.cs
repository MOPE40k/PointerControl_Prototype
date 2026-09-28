using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures
{
    public interface IDirectionalMovable : ITransformPosition
    {
        Vector3 CurrentVelocity { get; }
        Vector3 CurrentDestinationPoint { get; }

        void SetDestination(Vector3 position);
    }
}