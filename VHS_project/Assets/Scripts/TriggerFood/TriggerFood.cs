using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFood : MonoBehaviour
{
    public MouseController mouse;

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Food" && mouse.FoodPoint != null)
        {
            mouse.FoodPoint = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Food" && mouse.FoodPoint == null)
        {
            Debug.Log ("Food: " + other.name);

            mouse.FoodPoint = other.transform;
        }
    }
}
