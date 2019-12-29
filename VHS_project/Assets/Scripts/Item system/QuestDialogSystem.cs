using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDialogSystem : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public string QuestText;
    public string CompletedQuestText;
    public bool IsCompleted = false;

    public ItemType QuestItem;
    public GameObject RewardItem;

    // Start is called before the first frame update
    void Start()
    {
        Text.text = QuestText;
        if (RewardItem != null)
            if (RewardItem.GetComponent<Item>() == null)
                Debug.LogError (transform.name + ": RewardItem hasn't class Item");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckQuestStatus()
    {   if(Inventory.instance.IsItemEquipped)
            if (Inventory.instance.ActiveItem.Type == QuestItem && !IsCompleted)
            {
                IsCompleted = true;
                Text.text = CompletedQuestText;
                Inventory.instance.GiveItem (transform.position - new Vector3 (0, 100, 0));

                if (RewardItem != null)
                    Inventory.instance.EquipItem (RewardItem.GetComponent<Item> ());
            }
    }
}
