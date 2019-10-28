using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CookController : MonoBehaviour
{
    public enum State {Cooking = 0, Washing = 1, GiviningFood = 2}

    public Transform CookPoint;

    public Transform RelaxPoint;

    public Transform GivingFoodPoint;

    public Transform SaucepanObject;

    public GameObject[] SaucepanPositions;

    private SaucepanController saucepanController;

    private NavMeshAgent Agent;

    private State state = State.Cooking;

    private float timeBetweenStates = 15f;

    // Start is called before the first frame update
    void Start()
    {
        //if (SaucepanPositions.Length != 3)
          //  Debug.LogError ("SaucepanPositions length is " + SaucepanPositions.Length);
        Agent = GetComponent<NavMeshAgent> ();
        saucepanController = SaucepanObject.gameObject.GetComponent<SaucepanController> ();
        StartCoroutine (ChangeStateEnumerator (timeBetweenStates));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Cooking)
        {
            Cook ();
        } else if (state == State.GiviningFood)
        {
            GivingFood ();
        }
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
            case State.Washing:
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
            saucepanController.PickUp ();

        if (IsCloseToObject(SaucepanPositions[0], 2) && !ReachDestination(CookPoint.position))
        {
            LookAt (SaucepanPositions[0]);
        }
        else if (ReachDestination (CookPoint.position) && saucepanController.isPickedUp())
        {
            Debug.Log ("Reached Cook " + saucepanController.isPickedUp ());
            saucepanController.Drop ();
        }
    }

    private void GivingFood()
    {
        Agent.SetDestination (GivingFoodPoint.position);

        if (!ReachDestination (GivingFoodPoint.position))
            saucepanController.PickUp ();

        if (IsCloseToObject (SaucepanPositions[1], 2) && !ReachDestination (GivingFoodPoint.position))
        {
            LookAt (SaucepanPositions[1]);
        }
        else if (ReachDestination (GivingFoodPoint.position) && saucepanController.isPickedUp ())
        {
            Debug.Log ("Reached Give");
            saucepanController.Drop ();
        }
    }
}
