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
        if (IsItemEquipped && Input.GetMouseButtonDown (1))
        {
            DropItem ();
        }
    }

    public void EquipItem(Item item)
    {
        ActiveItem = item;
        InventoryUI.text = item.Type.ToString ();

        Debug.Log ("Get item");
        
        ActiveItem.GetComponent<Rigidbody> ().useGravity = false;
        ActiveItem.transform.position += new Vector3 (0, 100, 0);
        ActiveItem.transform.parent = Hands;
        StartCoroutine (PickUpEnumerator (0.1f));
    }

    public void DropItem()
    {
        IsItemEquipped = false;
        InventoryUI.text = "";

        Debug.Log ("Drop item");

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
        ActiveItem.GetComponent<Rigidbody> ().isKinematic = true;
        ActiveItem.transform.position = position;
        ActiveItem.transform.parent = null;

        ActiveItem = null;
    }

    public IEnumerator PickUpEnumerator(float delay)
    {
        yield return new WaitForSeconds (delay);

        IsItemEquipped = true;
    }
    
}
