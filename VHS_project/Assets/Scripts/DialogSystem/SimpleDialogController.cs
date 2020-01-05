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
    public QuestDialogSystem quest;

    public float Distance = 4f;

    // Start is called before the first frame update
    void Start()
    {
        dialogText = GetComponentInChildren<Canvas> ();
        player = GameObject.FindGameObjectWithTag ("Player");
        quest = GetComponent<QuestDialogSystem> ();
        dialogText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCloseToObject(player, Distance) && Input.GetKeyDown(KeyCode.F) )
        {
            if (quest != null)
                quest.CheckQuestStatus ();

            dialogText.enabled = true;
            StopAllCoroutines ();
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
            yield return null;
        }
        yield return new WaitForSeconds (dissapearTime);
        dialogText.enabled = false;

        yield break;
    }
}
