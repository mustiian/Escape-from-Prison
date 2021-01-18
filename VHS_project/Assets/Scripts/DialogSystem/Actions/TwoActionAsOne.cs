using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoActionAsOne : Action
{
    public Action First;

    public float Delay = 0.1f;

    public Action Second;

    public override void Activate()
    {
        StartCoroutine(ActionCoroutine(Delay));
    }

    private IEnumerator ActionCoroutine(float delay)
    {
        First.Activate();
        yield return new WaitForSeconds(delay);
        Second.Activate();
    }
}
