using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DialogueManager : MonoBehaviour
{
    public BubbleSpawner BubbleSpawner;

    public ResponseButtonsController buttonsController;

    private Dialogue ActiveDialogue;
    private FirstPersonController playerController;

    // Start is called before the first frame update
    public void StartDialogueSequence(Dialogue startDialogue)
    {
        playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ();
        playerController.SetMouseCursor (false);
        playerController.enabled = false;
        ActiveDialogue = startDialogue;
        ActicateDialogue ();
    }

    public void ActicateDialogue()
    {
        ActiveDialogue.StartDialogue (BubbleSpawner, buttonsController);        
    }

    public void EndDialogueSequence()
    {
        Debug.Log ("End all dialogues");
        playerController.enabled = true;
        playerController.SetMouseCursor (true);
    }

    public void NextDialogue(Dialogue dialogue)
    {
        ActiveDialogue.EndDialogue ();
        ActiveDialogue = dialogue;

        if (ActiveDialogue == null)
            EndDialogueSequence ();
        else
            ActicateDialogue ();
    }
}
