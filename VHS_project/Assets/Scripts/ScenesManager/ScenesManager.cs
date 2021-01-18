using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    public GameObject StartScene;
    public List<GameObject> AllScenes;

    // Start is called before the first frame update
    void Start()
    {
        ActivateScene (StartScene);
    }

    public void ActivateScene(GameObject scene)
    {
        DeacivateAllScenes ();
        scene.SetActive (true);
    }

    private void DeacivateAllScenes()
    {
        foreach (var scene in AllScenes)
        {
            scene.SetActive (false);
        }
    }
}
