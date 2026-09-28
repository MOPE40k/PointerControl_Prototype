using _Project.Develop.Gameplay.Features.MovementFeatures;
using UnityEngine;

namespace _Project.Develop.Gameplay.Features.InputFeatures.Controllers
{
    public class KeyboardCharacterMovableController : Controller
    {
        // References
        private readonly IDirectionalMovable _movable = null;

        public KeyboardCharacterMovableController(IDirectionalMovable movable)
            => _movable = movable;

        protected override void UpdateLogic(float deltaTime)
        {
            Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            _movable.SetDestination(inputDirection);
        }
    }
}