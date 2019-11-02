using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucepanController : MonoBehaviour
{
    public GameObject Item;
    public GameObject Parent;
    public Transform Guide;

    private Rigidbody rigidBody;
    private bool pick = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody> ();
    }

    public bool isPickedUp()
    {
        return pick;
    }

    // Update is called once per frame
    void Update()
    {
        if (pick)
        {
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;
            Item.transform.position = Guide.transform.position;
            Item.transform.rotation = Guide.transform.rotation;
            Item.transform.parent = Parent.transform;

            Debug.Log ("Pick");
        }
    }

    public void PickUp()
    {
        pick = true;
    }

    public void Drop()
    {
        pick = false;
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        Item.transform.parent = null;
        Item.transform.position = Guide.transform.position;

        Debug.Log ("Drop");
    }
}
