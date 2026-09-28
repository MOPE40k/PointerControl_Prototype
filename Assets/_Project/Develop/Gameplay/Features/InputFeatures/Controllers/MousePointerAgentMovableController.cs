using _Project.Develop.Gameplay.Characters;
using _Project.Develop.Utils.NavMeshManagment;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace _Project.Develop.Gameplay.Features.InputFeatures.Controllers
{
    public class MousePointerAgentMovableController : Controller
    {
        // Consts
        private const KeyCode RaycastButton = KeyCode.Mouse0;
        private const float RaycastMaxDistance = 100f;

        // Settings
        private readonly float _minDistanceToTarget = 0f;
        private readonly LayerMask _raycastMask = 0;

        // References
        private readonly AgentCharacter _character = null;
        private readonly Camera _raycastCamera = null;
        private readonly NavMeshPath _pathToTarget = null;

        // Runtime
        private Vector3 _targetPoint = Vector3.zero;

        public MousePointerAgentMovableController(
            AgentCharacter character,
            float minDistanceToTarget,
            LayerMask raycastMask,
            Camera raycastCamera = null)
        {
            _character = character;
            _minDistanceToTarget = minDistanceToTarget;
            _raycastMask = raycastMask;
            _raycastCamera = (raycastCamera == null)
                ? Camera.main
                : _raycastCamera;

            _pathToTarget = new NavMeshPath();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_character.IsDead)
                return;

            if (_character.InSpawnProcess(out float elapsedTime))
                return;

            if (_character.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
                if (_character.InJumpProcess == false)
                    _character.Jump(offMeshLinkData);

            if (Input.GetKeyDown(RaycastButton))
                MouseRaycast();

            if (_character.TryGetPath(_targetPoint, _pathToTarget))
                Move();
        }

        private void MouseRaycast()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (_character.IsStoppedMove)
                return;

            Ray mousePointerRay = _raycastCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mousePointerRay, out RaycastHit hit, RaycastMaxDistance, _raycastMask))
                _targetPoint = hit.point;
        }

        private void Move()
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReached(distanceToTarget))
                _character.SetDestination(_character.CurrentPosition);
            else
                _character.SetDestination(_targetPoint);
        }

        private bool IsTargetReached(float distanceToTarget)
            => distanceToTarget <= _minDistanceToTarget;
    }
}