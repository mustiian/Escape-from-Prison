using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveObjectAction : Action
{
    public NavMeshAgent Object;

    public Transform point;

    public override void Activate()
    {
        Object.SetDestination (point.position);
    }
}
