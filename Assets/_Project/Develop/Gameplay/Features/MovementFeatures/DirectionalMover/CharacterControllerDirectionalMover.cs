using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DirectionalMover
{
    public class CharacterControllerDirectionalMover : DirectionalMover
    {
        // References
        private readonly CharacterController _characterController = null;

        public CharacterControllerDirectionalMover(CharacterController characterController, float speed) : base(speed)
            => _characterController = characterController;

        public override void UpdateTick(float deltaTime)
            => Move(deltaTime);

        private void Move(float deltaTime)
            => _characterController.Move(CurrentVelocity * deltaTime);
    }
}