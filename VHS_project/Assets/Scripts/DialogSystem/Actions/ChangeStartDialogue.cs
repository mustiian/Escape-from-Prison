using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStartDialogue : Action
{
    public ActivateDialogue Dialogue;

    public Dialogue newDialogue;

    public override void Activate()
    {
        Dialogue.StartDialogue = newDialogue;
    }
}
