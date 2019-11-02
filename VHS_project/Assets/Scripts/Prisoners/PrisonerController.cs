using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerController : MonoBehaviour
{
    public enum State {Walking = 0, Waiting = 1}

    public NavMeshAgent Agent;
    public float Radius;

    private State state = State.Walking;
    private PrisonerRandomPosition randomPosition;

    // Start is called before the first frame update
    void Start()
    {
        randomPosition = GetComponent<PrisonerRandomPosition> ();
        randomPosition.FindNewPosition (Agent, Radius);
        StartCoroutine (ChangeStateEnumerator (5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Walking && !ReachDestination(Agent.pathEndPosition))
        {
            Debug.Log ("Walk");
        }
        else if (state == State.Waiting)
        {
            Debug.Log ("Wait");
        }
    }

    private void ChangeState()
    {
        switch (state)
        {
            case State.Walking:
                state = State.Waiting;
                break;
            case State.Waiting:
                state = State.Walking;
                break;
            default:
                break;
        }
    }

    private IEnumerator ChangeStateEnumerator(float timeOffset)
    {
        while (true)
        {
            yield return new WaitForSeconds (timeOffset);
            ChangeState ();
        }
    }

    private bool ReachDestination(Vector3 other)
    {
        if (other.x == transform.position.x && other.z == transform.position.z)
            return true;

        return false;
    }
}
