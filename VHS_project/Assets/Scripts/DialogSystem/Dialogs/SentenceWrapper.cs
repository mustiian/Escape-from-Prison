using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SentenceWrapper
{
    public string Sentence;

    public Transform Position;

    public SentenceType Type;

    public SentenceWrapper(string sentence, Transform position, SentenceType type)
    {
        Sentence = sentence;
        Position = position;
        Type = type;
    }
}

public enum SentenceType
{
    Monologue,
    Dialogue
}
