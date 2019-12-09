﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    public Dialogue StartDialogue;
    public float Distance = 3f;
    public Vector3 newRotation;

    private DialogueManager dm;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.FindGameObjectWithTag ("DialogueManager").GetComponent<DialogueManager> ();
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCloseToObject (player, Distance) && Input.GetKeyDown (KeyCode.E))
        {
            dm.BubbleSpawner.rotation = newRotation;
            dm.StartDialogueSequence (StartDialogue);
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
}
