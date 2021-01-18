using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeToSimpleDialogue : Action
{
    public SimpleDialogController DialogueController;

    public string Text;

    public override void Activate()
    {
        Debug.Log("Simpe Dialogue activated");
        DialogueController.dialogText.transform.rotation = new Quaternion(0, 0, 0, 0);
        DialogueController.dialogText.GetComponentInChildren<TextMeshProUGUI>().text = Text;
        DialogueController.enabled = true;
    }
}
