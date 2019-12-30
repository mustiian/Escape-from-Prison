using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SceneChangeTrigger : MonoBehaviour
{
    public GameObject SceneMapUI;
    private GameObject player;
    private FirstPersonController playerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerController = player.GetComponent<FirstPersonController> ();
    }

    private void Update()
    {
        if (IsCloseToObject(player, 3))
        {
            SceneMapUI.SetActive (true);
            Debug.Log ("Activate map");
        }
        else
        {
            SceneMapUI.SetActive (false);
            Debug.Log ("Deactivate map");
        }
    }

    private bool IsCloseToObject( GameObject target, float minDistance = 1 )
    {
        Vector3 gap = target.transform.position - transform.position;
        float distance = gap.sqrMagnitude;

        if (distance < minDistance * minDistance)
        {
            return true;
        }

        return false;
    }
}
