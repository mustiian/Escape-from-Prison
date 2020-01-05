using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    public Dialogue StartDialogue;
    public float Distance = 3f;
    public Vector3 newRotation;

    private DialogueManager dm;
    private GameObject player;

    public bool isUsed = false;


    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.FindGameObjectWithTag ("DialogueManager").GetComponent<DialogueManager> ();
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCloseToObject (player, Distance) && !isUsed)
        {
            isUsed = true;
            dm.BubbleSpawner.rotation = newRotation;
            dm.StartDialogueSequence (StartDialogue);
            StartCoroutine(RotatePlayerEnumerator(3f));
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

    private IEnumerator RotatePlayerEnumerator(float duration)
    {
        float startTime = Time.time;
        float step = 0;

        Transform playerTransformer = player.transform;
        var lookPos = transform.position - playerTransformer.position;
        var rotation = Quaternion.LookRotation(lookPos);

        while (startTime + duration > Time.time)
        {
            step += (1 / duration) * Time.deltaTime;

            playerTransformer.rotation = Quaternion.Lerp(playerTransformer.rotation, rotation, step);

            yield return null;
        }
    }
}
