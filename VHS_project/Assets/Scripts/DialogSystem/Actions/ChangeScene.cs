using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : Action
{
    public Scene SceneActivate;

    public override void Activate()
    {
        SceneActivate.ChangeScene ();
    }
}
