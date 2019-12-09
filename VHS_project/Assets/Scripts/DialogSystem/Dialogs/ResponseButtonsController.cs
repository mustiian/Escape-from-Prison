using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResponseButtonsController : MonoBehaviour
{
    public GameObject ButtonPrefab;

    public Canvas PlayerUI;

    private List<GameObject> ActiveButtons = new List<GameObject>();

    public Vector2 Offset;
    public Vector2 Position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddButton(Dialogue response, int index)
    {
        Vector3 position = PlayerUI.transform.position - new Vector3 (Position.x + index * Offset.x, Position.y + index * Offset.y, 0f);
        GameObject buttonObject = (GameObject)Instantiate (ButtonPrefab, position, PlayerUI.transform.rotation, PlayerUI.transform);
        Button button = buttonObject.GetComponent<Button> ();
        buttonObject.GetComponentInChildren<TextMeshProUGUI> ().SetText (response.ActiveSentence.Sentence);
        button.onClick.AddListener (response.ChooseDialogue);
        ActiveButtons.Add (button.gameObject);
    }

    public void UpdateButtons(List<Dialogue> responses)
    {
        DeleteButtons ();
        for (int i = 0; i < responses.Count; i++)
        {
            AddButton (responses[i], i);
        }
    }

    public void DeleteButtons()
    {
        foreach (var item in ActiveButtons)
        {
            Destroy (item.gameObject);
        }
        
        ActiveButtons = new List<GameObject> ();
    }
}
