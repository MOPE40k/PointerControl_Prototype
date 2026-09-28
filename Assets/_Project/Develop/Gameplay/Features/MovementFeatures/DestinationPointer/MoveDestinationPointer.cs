using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DestinationPointer
{
    public class MoveDestinationPointer
    {
        // Consts
        private const float MinDistanceToTarget = 1f;

        // Settings
        private readonly IDirectionalMovable _movable = null;

        // Runtime
        private readonly MoveDestinationPointerView _prefabInstance = null;

        public MoveDestinationPointer(IDirectionalMovable movable, MoveDestinationPointerView targetPointerPrefab)
        {
            _movable = movable;

            _prefabInstance = GameObject.Instantiate(targetPointerPrefab);
            _prefabInstance.Hide();
        }

        public void UpdateTick()
        {
            float distanceToTarget = CalculateXZDistance();

            if (IsTargetReached(distanceToTarget))
            {
                _prefabInstance.Hide();
            }
            else
            {
                _prefabInstance.SetPosition(_movable.CurrentDestinationPoint);
                _prefabInstance.Show();
            }
        }

        private float CalculateXZDistance()
        {
            Vector3 currentPosition = _movable.CurrentPosition;
            currentPosition.y = 0f;

            Vector3 destinationPosition = _movable.CurrentDestinationPoint;
            destinationPosition.y = 0f;

            return Vector3.Distance(currentPosition, destinationPosition);
        }

        private bool IsTargetReached(float distanceToTarget)
            => distanceToTarget <= MinDistanceToTarget;
    }
}