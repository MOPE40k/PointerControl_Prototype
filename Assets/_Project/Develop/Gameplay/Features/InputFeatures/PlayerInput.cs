using _Project.Develop.Gameplay.Characters;
using _Project.Develop.Gameplay.Features.InputFeatures.Controllers;
using UnityEngine;

namespace _Project.Develop.Gameplay.Features.InputFeatures
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField, Min(0.1f)] private float _minDistanceToTarget = 1f;
        [SerializeField] private LayerMask _raycastMask = 0;

        [Space]
        [Header("References:")]
        [SerializeField] private AgentCharacter _character = null;
        [SerializeField] private Camera _raycastCamera = null;

        // References
        private Controller _mouseInputController = null;

        private void Awake()
        {
            _mouseInputController = new CompositeController(new Controller[]{
                new MousePointerAgentMovableController(
                    _character,
                    _minDistanceToTarget,
                    _raycastMask,
                    _raycastCamera),

                new AlongDirectionRotatableController(
                    _character,
                    _character)});
        }

        private void OnEnable()
            => _mouseInputController.Enable();

        private void OnDisable()
            => _mouseInputController.Disable();

        private void Update()
            => _mouseInputController.UpdateTick(Time.deltaTime);
    }
}