using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item ActiveItem;

    public bool IsItemEquipped = false;

    public Transform Hands;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsItemEquipped && Input.GetKeyDown (KeyCode.E))
        {
            DropItem ();
        }
    }

    public void EquipItem(Item item)
    {
        if (IsCloseToObject(item.gameObject, 2))
        {
            ActiveItem = item;
            IsItemEquipped = true;

            ActiveItem.GetComponent<Rigidbody> ().useGravity = false;
            ActiveItem.transform.position = new Vector3 (0, 500, 0);
            ActiveItem.transform.parent = Hands;
        }
    }

    public void DropItem()
    {
        IsItemEquipped = false;

        ActiveItem.GetComponent<Rigidbody> ().useGravity = true;
        ActiveItem.transform.position = Hands.position;
        ActiveItem.transform.parent = null;

        ActiveItem = null;
    }

    private bool IsCloseToObject( GameObject target, float minDistance = 1 )
    {
        Vector3 gap = target.transform.position - transform.position;
        float distance = gap.sqrMagnitude;

        if (distance < minDistance * minDistance)
        {
            return true;
        }

        return false;
    }
}
