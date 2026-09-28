using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Develop.Gameplay.Features.MovementFeatures
{
    public class AgentJumper
    {
        // Settings
        private readonly float _speed = 0f;

        // References
        private readonly NavMeshAgent _navMeshAgent = null;
        private readonly MonoBehaviour _coroutineRunner = null;
        private readonly AnimationCurve _yOffsetCurve = null;

        // Runtime
        private Coroutine _jumpProcess = null;

        public AgentJumper(
            float speed, 
            NavMeshAgent navMeshAgent, 
            MonoBehaviour corutineRunner, 
            AnimationCurve yOffsetCurve)
        {
            _speed = speed;
            _navMeshAgent = navMeshAgent;
            _coroutineRunner = corutineRunner;
            _yOffsetCurve = yOffsetCurve;

            _navMeshAgent.autoTraverseOffMeshLink = false;
        }

        public bool InProcess => _jumpProcess != null;

        public void Jump(OffMeshLinkData offMeshLinkData)
        {
            if (InProcess)
                return;

            _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
        }

        private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
        {
            Vector3 startPosition = _navMeshAgent.transform.position;
            Vector3 endPosition = offMeshLinkData.endPos;

            float duration = Vector3.Distance(startPosition, endPosition) / _speed;

            float progress = 0f;

            while (progress < duration)
            {
                float yOffset = _yOffsetCurve.Evaluate(progress / duration);

                _navMeshAgent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / duration) + Vector3.up * yOffset;

                progress += Time.deltaTime;

                yield return null;
            }

            _navMeshAgent.CompleteOffMeshLink();

            _jumpProcess = null;
        }
    }
}