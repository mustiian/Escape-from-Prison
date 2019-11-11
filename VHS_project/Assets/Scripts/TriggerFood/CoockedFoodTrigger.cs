using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoockedFoodTrigger : MonoBehaviour
{
    public bool Prepared;
    public bool Taken;

    public CookController Cooker;

    private PrisonerController[] prisoners;

    private void Start()
    {
        prisoners = FindObjectsOfType<PrisonerController> ();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Food")
        {
            Prepared = false;
            Taken = true;
            Cooker.CreateNewSaucepan ();
            Cooker.StartCooking ();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Food")
        {
            for (int i = 0; i < 10; i++)
            {
                int index = Random.Range (0, prisoners.Length);

                if (prisoners[index].isHungry)
                {
                    prisoners[index].StartEatState (other.gameObject);
                    Debug.Log ("Food is here");
                    return;
                }
            }
           
        }
    }
}
