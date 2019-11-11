using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject Item;
    public GameObject Parent;
    public Transform Guide;

    // Start is called before the first frame update
    void Start()
    {
        Item.GetComponent<Rigidbody> ().useGravity = true;
    }

    private void OnMouseDown()
    {
        Item.GetComponent<Rigidbody> ().useGravity = false;
        Item.GetComponent<Rigidbody> ().isKinematic = true;
        Item.transform.position = Guide.transform.position;
        Item.transform.rotation = Guide.transform.rotation;
        Item.transform.parent = Parent.transform;
    }

    private void OnMouseUp()
    {
        Item.GetComponent<Rigidbody> ().useGravity = true;
        Item.GetComponent<Rigidbody> ().isKinematic = false;
        Item.transform.parent = null;
        Item.transform.position = Guide.transform.position;
    }
}
