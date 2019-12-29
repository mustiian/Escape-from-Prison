﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    private Camera mainCamera;

    private Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        playerInventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            var ray = mainCamera.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "UI")
                {
                    var button = hit.transform.GetComponent<Button> ();
                    button.onClick.Invoke ();
                }
            }
        }

        if (Input.GetKeyDown (KeyCode.E))
        {
            var ray = mainCamera.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast (ray, out hit))
            {
                Item item = hit.transform.gameObject.GetComponent<Item> ();

                if (item != null && !playerInventory.IsItemEquipped)
                {
                    playerInventory.EquipItem (item);
                }
            }
        }
    }
}
