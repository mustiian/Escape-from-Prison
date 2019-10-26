using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseController : MonoBehaviour
{
    public enum State {Walking = 0, Eating = 1, Sleeping = 2}

    public Transform[] MovementPoints;

    public NavMeshAgent Agent;

    public Transform SavePoint;

    public Transform FoodPoint;

    private GameObject player;

    private State state = State.Walking;

    private Transform actualPoint = null;

    private float distanceToPlayer = 3f;
    private float stayingTime = 3;
    private float timeBetweenMovement = 3f;
    private float timeBetweenStates = 30f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        StartCoroutine (ChangeStateEnumerator (timeBetweenStates));
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (IsCloseToObject (player, distanceToPlayer))
            {
                state = State.Sleeping;
            }

            if (state == State.Walking)
            {
                WalkState ();
                Debug.Log ("Walk state");
            } else if (state == State.Sleeping)
            {
                SleepState ();
                Debug.Log ("Sleep state");

            }
            else if (state == State.Eating)
            {
                EatState ();
                Debug.Log ("Eat state");
            }
        }
    }

    private bool IsCloseToObject(GameObject target, float minDistance = 1)
    {
        Vector3 gap = target.transform.position - transform.position;
        float distance = gap.sqrMagnitude;

        if (distance < minDistance * minDistance)
        {
            Agent.SetDestination (SavePoint.position);
            return true;
        }

        return false;
    }

    private void ChangeState()
    {
        switch (state)
        {
            case State.Walking:
                state = State.Eating;
                break;
            case State.Eating:
                state = State.Sleeping;
                break;
            case State.Sleeping:
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

    private void WalkState()
    {
        int index = 0;
        if (actualPoint == null)
        {
            while (actualPoint != MovementPoints[index])
            {
                index = (int)Random.Range (0, MovementPoints.Length);
                actualPoint = MovementPoints[index];
            }
        }
        else
        {
            if (IsCloseToObject(actualPoint.gameObject, 0.2f))
            {
                if (stayingTime == 0)
                    stayingTime = Time.time + timeBetweenMovement;

                if (Time.time > stayingTime)
                {
                    index = (int)Random.Range (0, MovementPoints.Length);
                    actualPoint = MovementPoints[index];
                    stayingTime = 0;
                }
            }
        }
        Agent.SetDestination (actualPoint.position);
    }

    private void SleepState()
    {
        Agent.SetDestination (SavePoint.position);
    }

    private void EatState()
    {
        if (FoodPoint)
            Agent.SetDestination (FoodPoint.position);
        else
            WalkState ();
    }
}
