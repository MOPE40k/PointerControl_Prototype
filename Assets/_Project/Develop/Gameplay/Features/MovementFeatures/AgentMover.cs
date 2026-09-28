using UnityEngine;
using UnityEngine.AI;

namespace _Project.Develop.Gameplay.Features.MovementFeatures
{
    public class AgentMover
    {
        // References
        private readonly NavMeshAgent _agent = null;

        // Runtime
        public Vector3 CurrentDestination { get; private set; } = Vector3.zero;

        public AgentMover(NavMeshAgent agent, float speed)
        {
            _agent = agent;
            _agent.speed = speed;
            _agent.acceleration = 999f;
        }

        // Runtime
        public Vector3 CurrentVelocity => _agent.desiredVelocity;
        public bool IsStopped => _agent.isStopped;

        public void SetMoveDestination(Vector3 position)
        {
            CurrentDestination = position;

            _agent.SetDestination(CurrentDestination);
        }

        public void Resume()
            => _agent.isStopped = false;

        public void Stop()
            => _agent.isStopped = true;
    }
}