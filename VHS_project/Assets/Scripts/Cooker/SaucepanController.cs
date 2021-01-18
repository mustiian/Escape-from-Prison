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

    public void Set(GameObject item, GameObject parent, Transform guide)
    {
        Item = item;
        Parent = parent;
        Guide = guide;
    }

    // Update is called once per frame
    void Update()
    {
        if (pick)
        {
            Item.transform.position = Guide.transform.position;
            Item.transform.rotation = Guide.transform.rotation;
        }
    }

    public void PickUp()
    {
        pick = true;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        Item.transform.parent = Parent.transform;
    }

    public void Drop()
    {
        pick = false;
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        Item.transform.parent = null;
        Item.transform.position = Guide.transform.position;
        rigidBody.velocity = Vector3.zero;
    }
}
