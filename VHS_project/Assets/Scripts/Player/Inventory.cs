using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Item ActiveItem;

    public bool IsItemEquipped = false;

    public Transform Hands;

    public static Inventory instance;

    public TextMeshProUGUI InventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        InventoryUI.text = "";
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
        ActiveItem = item;
        IsItemEquipped = true;
        InventoryUI.text = item.Type.ToString ();

        ActiveItem.GetComponent<Rigidbody> ().useGravity = false;
        ActiveItem.transform.position = new Vector3 (0, 500, 0);
        ActiveItem.transform.parent = Hands;
    }

    public void DropItem()
    {
        IsItemEquipped = false;
        InventoryUI.text = "";
        ActiveItem.GetComponent<Rigidbody> ().useGravity = true;
        ActiveItem.transform.position = Hands.position;
        ActiveItem.transform.parent = null;

        ActiveItem = null;
    }

    public void GiveItem(Vector3 position)
    {
        IsItemEquipped = false;
        InventoryUI.text = "";
        ActiveItem.GetComponent<Rigidbody> ().useGravity = false;
        ActiveItem.transform.position = position;
        ActiveItem.transform.parent = null;

        ActiveItem = null;
    }

    
}
