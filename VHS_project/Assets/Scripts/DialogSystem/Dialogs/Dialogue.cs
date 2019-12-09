using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public SentenceWrapper ActiveSentence;

    public Dialogue NextDialogue { get; private set; }

    public bool isInteractive;
    public bool isAnswer;

    public bool isActive { get; private set; }

    [Range(0, 10)]
    public float Delay = 2f;

    public List<Action> EndActions;

    public List<Dialogue> Responses;

    private ResponseButtonsController responseButtons;

    public void StartDialogue(BubbleSpawner bubble, ResponseButtonsController buttons)
    {
        Debug.Log ("    Start: " + ActiveSentence.Sentence);
        isActive = true;

        bubble.Spawn (ref ActiveSentence.Position, ActiveSentence.Sentence, Delay, true);

        if (isInteractive)
        {
            responseButtons = buttons;
            StartCoroutine (ResponseButtonsEnumarator (Delay));
        } else
        {
            StartCoroutine (ResponseEnumarator (Delay, 0));
        }
    }

    public void EndDialogue()
    {
        Debug.Log ("    End" + ActiveSentence.Sentence);
        if (isInteractive)
            responseButtons.DeleteButtons ();

        if (EndActions.Count > 0)
            foreach (var action in EndActions)
            {
                action.Activate ();
            }

        isActive = false;
    }

    public void AddResponse(Dialogue responce)
    {
        Responses.Add (responce);
    }

    public void DeleteResponse(Dialogue responce)
    {
        Responses.Remove (responce);
    }

    public string GetSentenceText()
    {
        return ActiveSentence.Sentence;
    }

    public void ChooseDialogue()
    {
        if (isAnswer){
            var dm = GameObject.FindGameObjectWithTag ("DialogueManager").GetComponent<DialogueManager> ();
            if (Responses.Count > 0)
                Responses[0].ChooseDialogue ();
        }
        else
        {
            var dm = GameObject.FindGameObjectWithTag ("DialogueManager").GetComponent<DialogueManager> ();
            dm.NextDialogue (this);
        }
        
    }

    private IEnumerator ResponseEnumarator(float delay, int index)
    {
        yield return new WaitForSeconds (delay);
        if (Responses.Count > 0)
            Responses[index].ChooseDialogue ();
        else
        {
            var dm = GameObject.FindGameObjectWithTag ("DialogueManager").GetComponent<DialogueManager> ();
            dm.NextDialogue (null);
        }

    }

    private IEnumerator ResponseButtonsEnumarator(float delay)
    {
        yield return new WaitForSeconds (delay);
        responseButtons.UpdateButtons (Responses);
    }
}
