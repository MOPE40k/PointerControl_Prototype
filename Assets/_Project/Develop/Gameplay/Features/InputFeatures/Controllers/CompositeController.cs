namespace _Project.Develop.Gameplay.Features.InputFeatures.Controllers
{
    public class CompositeController : Controller
    {
        // References
        private readonly Controller[] _controllers = null;

        public CompositeController(Controller[] controllers)
            => _controllers = controllers;

        public override void Enable()
        {
            base.Enable();

            foreach (Controller controller in _controllers)
                controller.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            foreach (Controller controller in _controllers)
                controller.Disable();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            foreach (Controller controller in _controllers)
                controller.UpdateTick(deltaTime);
        }
    }
}