using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseController : MonoBehaviour
{
    public enum State {Walking = 0, Eating = 1, Relaxing = 2}

    public Transform[] MovementPoints;

    public NavMeshAgent Agent;

    public Transform SavePoint;

    public Transform FoodPoint;

    private GameObject player;

    private State state = State.Walking;

    private Transform actualPoint = null;

    private Animator animator;

    private float distanceToPlayer = 3f;
    private float stayingTime = 3;
    private float timeBetweenMovement = 5f;
    private float timeBetweenStates = 15f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        animator = GetComponent<Animator> ();
        StartCoroutine (ChangeStateEnumerator (timeBetweenStates));
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (IsCloseToObject (player, distanceToPlayer))
            {
                state = State.Relaxing;
            }

            if (state == State.Walking)
            {
                WalkState ();
            }
            else if (state == State.Relaxing) 
            {
                SleepState ();
            }
            else if (state == State.Eating)
            {
                EatState ();
            }
        }
    }

    private bool IsCloseToObject(GameObject target, float minDistance = 1)
    {
        Vector3 gap = target.transform.position - transform.position;
        float distance = gap.sqrMagnitude;

        if (distance < minDistance * minDistance)
        {
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
                state = State.Relaxing;
                break;
            case State.Relaxing:
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
            index = (int)Random.Range (0, MovementPoints.Length);
            actualPoint = MovementPoints[index];
            animator.SetTrigger ("Walk");

        }
        else
        {
            if (IsCloseToObject(actualPoint.gameObject, 0.2f))
            {
                animator.SetTrigger ("Idle");

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
        animator.SetTrigger ("Idle");
    }

    private void EatState()
    {
        if (FoodPoint)
        {
            if (!IsCloseToObject (FoodPoint.gameObject, 1f))
            {
                Agent.SetDestination (FoodPoint.position);
                animator.SetTrigger ("Walk");
            }
        }
        else
        {
            WalkState ();
            animator.SetTrigger ("Idle");
        }
    }
}
