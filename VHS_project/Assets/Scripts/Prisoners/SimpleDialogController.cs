using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogController : MonoBehaviour
{
    public Canvas dialogText;
    private GameObject player;
    private float speedRotation = 10f;
    private float dissapearTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        dialogText = GetComponentInChildren<Canvas> ();
        player = GameObject.FindGameObjectWithTag ("Player");

        dialogText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCloseToObject(player, 3) && Input.GetKeyDown(KeyCode.E) )
        {
            dialogText.enabled = true;
            StopAllCoroutines ();
            Debug.Log("Talk with player");
            LookAt (player);
        }
    }

    private bool ReachDestination( Vector3 other )
    {
        if (other.x == transform.position.x && other.z == transform.position.z)
            return true;

        return false;
    }

    private void LookAt( GameObject other )
    {
        var lookPos = other.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation (lookPos);
        StartCoroutine (rotateToObject (rotation));
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

    private IEnumerator rotateToObject(Quaternion destRotation)
    {
        int length = 20;
        for (int i = 0; i < length; i++)
        {
            transform.rotation = Quaternion.Slerp (transform.rotation, destRotation, speedRotation * Time.deltaTime);
            Debug.Log ("Rotate to the player" + transform.rotation.y + " | Dest: " + destRotation.y + " Inverse " + Quaternion.Inverse(destRotation).y);
            yield return null;
        }
        Debug.Log ("Wait");

        yield return new WaitForSeconds (dissapearTime);

        Debug.Log ("Disable");


        dialogText.enabled = false;

        yield break;
    }
}
