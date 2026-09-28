namespace _Project.Develop.Gameplay.Features.InputFeatures.Controllers
{
    public abstract class Controller
    {
        // Runtime
        private bool _isEnabled = false;

        public virtual void Enable()
            => _isEnabled = true;

        public virtual void Disable()
            => _isEnabled = false;

        public void UpdateTick(float deltaTime)
        {
            if (_isEnabled == false)
                return;

            UpdateLogic(deltaTime);
        }

        protected abstract void UpdateLogic(float deltaTime);
    }
}