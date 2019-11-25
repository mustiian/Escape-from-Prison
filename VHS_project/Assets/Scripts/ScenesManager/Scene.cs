using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class Scene : MonoBehaviour
{
    public string Name;
    public Transform respawnPosition;
    public Quaternion respawnRotation;

    private GameObject player;
    private GameObject fader;
    private FirstPersonController playerController;
    private ScenesManager sceneManager;
    private GameObject sceneGameObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerController = player.GetComponent<FirstPersonController> ();
        fader = GameObject.FindGameObjectWithTag ("Fader");
        sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<ScenesManager>();
        sceneGameObject = transform.GetChild (0).gameObject;
    }

    public void ChangeScene()
    {
        StartCoroutine (changeSceneEnumerator ());
    }

    private IEnumerator changeSceneEnumerator()
    {
        fader.GetComponent<Animator> ().SetTrigger ("fadein");

        yield return new WaitForSeconds (1f);
        //sceneManager.ActivateScene (sceneGameObject);

        playerController.enabled = false;
        player.transform.rotation = respawnRotation;
        player.transform.position = respawnPosition.position;

        yield return new WaitForSeconds (1.0f);

        fader.GetComponent<Animator> ().SetTrigger ("fadeout");

        playerController.enabled = true;
        playerController.SetMouseCursor(true);
    }
}
