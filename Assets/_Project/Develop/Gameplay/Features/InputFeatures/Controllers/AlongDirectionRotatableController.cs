using _Project.Develop.Gameplay.Features.MovementFeatures;

namespace _Project.Develop.Gameplay.Features.InputFeatures.Controllers
{
    public class AlongDirectionRotatableController : Controller
    {
        // References
        private readonly IDirectionalRotatable _rotatable = null;
        private readonly IDirectionalMovable _movable = null;

        public AlongDirectionRotatableController(IDirectionalRotatable rotatable, IDirectionalMovable movable)
        {
            _rotatable = rotatable;
            _movable = movable;
        }

        protected override void UpdateLogic(float deltaTime)
            => _rotatable.SetRotateDirection(_movable.CurrentVelocity);
    }
}