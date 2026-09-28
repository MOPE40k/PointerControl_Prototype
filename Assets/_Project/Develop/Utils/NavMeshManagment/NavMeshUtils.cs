using UnityEngine;
using UnityEngine.AI;

namespace _Project.Develop.Utils.NavMeshManagment
{
    public static class NavMeshUtils
    {
        public static float GetPathLength(NavMeshPath path)
        {
            float pathLength = 0f;

            if (path.corners.Length > 1)
                for (int i = 1; i < path.corners.Length; i++)
                    pathLength += Vector3.Distance(path.corners[i - 1], path.corners[i]);

            return pathLength;
        }

        public static float GetPathLength(NavMeshAgent agent)
            => agent.remainingDistance;

        public static bool TryGetPath(
            Vector3 sourcePosition,
            Vector3 targetPosition,
            NavMeshQueryFilter filter,
            NavMeshPath path)
        {
            if (NavMesh.CalculatePath(sourcePosition, targetPosition, filter, path) && path.status != NavMeshPathStatus.PathInvalid)
                return true;

            return false;
        }

        public static bool TryGetPath(NavMeshAgent agent, Vector3 targetPosition, NavMeshPath path)
        {
            if (agent.CalculatePath(targetPosition, path) && path.status != NavMeshPathStatus.PathInvalid)
                return true;

            return false;
        }
    }
}