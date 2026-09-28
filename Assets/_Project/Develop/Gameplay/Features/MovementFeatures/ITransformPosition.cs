using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures
{
    public interface ITransformPosition
    {
        Vector3 CurrentPosition { get; }
    }
}