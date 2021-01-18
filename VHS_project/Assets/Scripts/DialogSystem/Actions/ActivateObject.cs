using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : Action
{
    public GameObject Object;

    public override void Activate()
    {
        Object.SetActive (true);
    }
}
