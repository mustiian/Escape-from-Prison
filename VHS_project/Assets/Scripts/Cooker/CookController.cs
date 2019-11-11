using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CookController : MonoBehaviour
{
    public enum State {Cooking = 0, Relaxing = 1, GiviningFood = 2}

    public Transform CookPoint;

    public Transform RelaxPoint;

    public Transform GivingFoodPoint;

    public GameObject SaucepanPrefab;

    public Transform Hands;

    public GameObject[] SaucepanPositions;

    private GameObject SaucepanObject;

    private SaucepanController saucepanController;

    private NavMeshAgent Agent;

    private Animator animator;

    private State state = State.Cooking;

    private float timeBetweenStates = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent> ();
        animator = GetComponent<Animator> ();
        CreateNewSaucepan ();
        StartCoroutine (ChangeStateEnumerator (timeBetweenStates));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Cooking && SaucepanObject != null)
        {
            Cook ();
        }
        else if (state == State.GiviningFood && SaucepanObject != null)
        {
            GivingFood ();
        }
        else if (state == State.Relaxing)
        {
            Relaxing ();
        }
    }

    public void StartCooking()
    {
        state = State.Cooking;
        StopAllCoroutines ();
        StartCoroutine (ChangeStateEnumerator (timeBetweenStates));
    }

    private void ChangeState()
    {
        switch (state)
        {
            case State.Cooking:
                state = State.GiviningFood;
                break;
            case State.GiviningFood:
                state = State.Cooking;
                break;
            case State.Relaxing:
                state = State.Cooking;
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

    private void LookAt(GameObject other)
    {
        var lookPos = other.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation (lookPos);
        transform.rotation = Quaternion.Lerp (transform.rotation, rotation, 15 * Time.deltaTime);
    }

    private bool ReachDestination(Vector3 other)
    {
        if (other.x == transform.position.x && other.z == transform.position.z)
            return true;

        return false;
    }

    private void Cook()
    {
        Agent.SetDestination (CookPoint.position);

        if (!ReachDestination (CookPoint.position))
        {
            animator.SetTrigger ("Walk");
            saucepanController.PickUp ();
        }
        else
        {
            animator.SetTrigger ("Idle");
        }

        if (IsCloseToObject(SaucepanPositions[0], 2) && !ReachDestination(CookPoint.position))
        {
            LookAt (SaucepanPositions[0]);
        }
        else if (ReachDestination (CookPoint.position) && saucepanController.isPickedUp())
        {
            saucepanController.Drop ();
            animator.SetTrigger ("Idle");
        }
    }

    private void GivingFood()
    {
        Agent.SetDestination (GivingFoodPoint.position);

        if (!ReachDestination (GivingFoodPoint.position))
        {
            saucepanController.PickUp ();
            animator.SetTrigger ("Walk");
        }

        if (IsCloseToObject (SaucepanPositions[1], 2) && !ReachDestination (GivingFoodPoint.position))
        {
            LookAt (SaucepanPositions[1]);
        }
        else if (ReachDestination (GivingFoodPoint.position) && saucepanController.isPickedUp ())
        {
            saucepanController.Drop ();
            SaucepanObject = null;
            StopAllCoroutines ();
            state = State.Relaxing;
        }
    }

    private void Relaxing()
    {
        Agent.SetDestination (RelaxPoint.position);
        animator.SetTrigger ("Idle");

        if (IsCloseToObject (SaucepanPositions[2], 2) && !ReachDestination (RelaxPoint.position))
        {
            LookAt (SaucepanPositions[2]);
        }
    }

    public void CreateNewSaucepan()
    {
        if (SaucepanObject == null)
        {
            SaucepanObject = GameObject.Instantiate (SaucepanPrefab);
            saucepanController = SaucepanObject.gameObject.GetComponent<SaucepanController> ();
            saucepanController.Set (SaucepanObject, Hands.gameObject, Hands);
        }
    }
}
