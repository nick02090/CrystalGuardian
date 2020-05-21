using UnityEngine;
using UnityEngine.AI;

namespace CG.Control
{
    public class WaypointPatrol : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public Transform[] waypoints;

        int currentWaypointIndex;

        private void Start()
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }

        private void Update()
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
            }
        }
    }
}
