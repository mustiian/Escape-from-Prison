using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject BubblePrefab;

    public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn (ref Transform position, string text, float delay, bool isDynamic)
    {
        position.rotation = Quaternion.Euler (rotation.x, rotation.y, rotation.z);

        GameObject bubbleInstance = (GameObject)Instantiate (BubblePrefab, position.transform.position, position.transform.rotation, position);

        TextMeshProUGUI UItext = bubbleInstance.GetComponentInChildren<TextMeshProUGUI> ();
        Bubble bubble = bubbleInstance.GetComponentInChildren<Bubble> ();

        bubble.Delay = delay;
        bubble.isDynamic = isDynamic;
        bubble.SetPosition (ref position);
        UItext.SetText (text);
    }

}
