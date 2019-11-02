using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerController : MonoBehaviour
{
    public NavMeshAgent Agent;

    public float Radius;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (FindNewPointEnumeratror());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool RandomNavmeshLocation(Vector3 center, float range, out Vector3 result)
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

    private IEnumerator FindNewPointEnumeratror()
    {
        while (true)
        {
            RandomNavmeshLocation (transform.position, Radius, out target);
            if (Agent.pathStatus == NavMeshPathStatus.PathComplete)
                Agent.SetDestination (target);

            yield return new WaitForSeconds (5f);
        }
    }

    private void OnDrawGizmos()
    {
        if (Agent.hasPath)
            Gizmos.DrawWireSphere (target, 0.5f);
    }

}
