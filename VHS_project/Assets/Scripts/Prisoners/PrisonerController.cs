using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerController : MonoBehaviour
{
    public enum State {Walking = 0, Waiting = 1, Eating = 2, TakingFood = 3, GivingFood = 4}

    public NavMeshAgent Agent;
    public float Radius;

    public Transform TakeFoodPoint;
    public Transform GiveFoodPoint;
    public Transform Hands;
    public bool isHungry { get; private set; } = true;

    public Transform[] RandomPoints;

    private GameObject foodObject;
    private State state = State.Walking;
    private PrisonerRandomPosition randomPosition;
    private DinnerSeatsController seatsController;
    private Seat seat;
    private Vector3 latestPosition;
    private Animator animator;

    private float findPathTime = 0f;
    private float WaitingTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        randomPosition = GetComponent<PrisonerRandomPosition> ();
        seatsController = FindObjectOfType<DinnerSeatsController> ();
        animator = GetComponent<Animator> ();
        randomPosition.FindNewPosition (Agent, Radius);
        StartCoroutine (ChangeStateEnumerator (10f));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Walking)
        {
            if (!Agent.hasPath || findPathTime > 4f)
            {
                if (foodObject != null)
                {
                    GameObject.Destroy(foodObject);
                    foodObject = null;
                }

                randomPosition.FindNewPosition (Agent, Radius);
                findPathTime = 0f;
            }
            else
            {
                if (Agent.pathStatus == NavMeshPathStatus.PathPartial)
                {
                    randomPosition.FindNewPosition(Agent, Radius);
                    findPathTime = 0f;
                }

                findPathTime += Time.deltaTime;
                animator.SetTrigger ("Walk");
            }
        }
        else if (state == State.Waiting)
        {
            animator.SetTrigger ("Idle");
            WaitingTime += Time.deltaTime;

            if (WaitingTime > 12f)
            {
                WaitingTime = 0f;
                int index = Random.Range(0, RandomPoints.Length);
                Agent.SetDestination(RandomPoints[index].position);
            }
        }
        else if (state == State.TakingFood)
        {
            TakeFood ();
        }
        else if (state == State.Eating)
        {
            animator.SetTrigger ("Eat");

            Eat ();
        }
        else if (state == State.GivingFood)
        {
            GiveFood ();
        }
    }

    public void StartEatState(GameObject food)
    {
        StopAllCoroutines ();
        state = State.TakingFood;
        isHungry = false;
        foodObject = food;
        seat = seatsController.GetFreeSeat ();
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
            case State.TakingFood:
                state = State.Eating;
                break;
            case State.Eating:
                state = State.GivingFood;
                foodObject.GetComponent<SaucepanController> ().PickUp ();
                seatsController.SetSeatToFree (seat);
                break;
            case State.GivingFood:
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

    private void LookAt(GameObject other)
    {
        var lookPos = other.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation (lookPos);
        transform.rotation = Quaternion.Lerp (transform.rotation, rotation, 15 * Time.deltaTime);
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

    private void TakeFood()
    {
        if (Hands.childCount == 0)
        {
            animator.SetTrigger ("Walk");

            Agent.SetDestination (TakeFoodPoint.position);


            if (IsCloseToObject (foodObject, 2) && !ReachDestination (TakeFoodPoint.position))
            {
                LookAt (foodObject);
            }
            else if (IsCloseToObject(foodObject, 1f) || ReachDestination (TakeFoodPoint.position))
            {
                foodObject.GetComponent<SaucepanController> ().Set (foodObject, Hands.gameObject, Hands);
                foodObject.GetComponent<SaucepanController> ().PickUp ();
            }
        }
        else
        {
            Agent.SetDestination (seat.SeatPoint.position);

            if (ReachDestination (seat.SeatPoint.position))
            {
                ChangeState ();
                animator.SetTrigger ("Eat");
                transform.rotation = seat.SeatPoint.rotation;
                StartCoroutine (ChangeStateEnumerator (10f));
                foodObject.GetComponent<SaucepanController> ().Drop ();
            }
        }
    }

    private void Eat()
    {
    }

    private void GiveFood()
    {
        Agent.SetDestination (GiveFoodPoint.position);
        animator.SetTrigger ("Walk");

        if (ReachDestination (GiveFoodPoint.position) || IsCloseToObject(GiveFoodPoint.gameObject, 3f))
        {
            ChangeState ();
            GameObject.Destroy (foodObject);
            foodObject = null;
        }
    }
}
