using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAction : Action
{
    public GameObject Object;

    public float Delay;

    public override void Activate()
    {
        StartCoroutine(WaitEnumerator(Delay));
    }

    private IEnumerator WaitEnumerator(float delay)
    {
        yield return new WaitForSeconds(delay);

        Object.SetActive(false);
    }
}
