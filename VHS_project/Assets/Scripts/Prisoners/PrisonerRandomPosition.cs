using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerRandomPosition : MonoBehaviour
{
    private Vector3 target;

    private bool RandomNavmeshLocation( Vector3 center, float range, out Vector3 result )
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition (randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void FindNewPosition(NavMeshAgent agent, float raduis)
    {
        StartCoroutine (FindNewPointEnumeratror (agent, raduis));
    }

    private IEnumerator FindNewPointEnumeratror(NavMeshAgent agent, float raduis )
    {
        while (true)
        {
            RandomNavmeshLocation (transform.position, raduis, out target);
            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
                agent.SetDestination (target);

            yield return new WaitForSeconds (5f);
        }
    }

    private void OnDrawGizmos()
    {
        if (target != Vector3.zero)
            Gizmos.DrawWireSphere (target, 0.5f);
    }
}
