using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : Action
{
    public Transform Object;
    public Quaternion Rotation;
    public float Duration = 2f;

    public override void Activate()
    {
        StartCoroutine(RotateCorotine(Duration));
    }

    private IEnumerator RotateCorotine(float duration)
    {
        float startTime = Time.time;
        float step = 0;

        while (startTime + duration > Time.time)
        {
            step += (1 / duration) * Time.deltaTime;

            Object.rotation = Quaternion.Lerp(Object.rotation, Rotation, step);

            yield return null;
        }
    }

}
