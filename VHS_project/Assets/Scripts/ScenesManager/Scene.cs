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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerController = player.GetComponent<FirstPersonController> ();
        fader = GameObject.FindGameObjectWithTag ("Fader");
    }

    public void ChangeScene()
    {
        playerController.enabled = false;
        StartCoroutine (changeSceneEnumerator ());
    }

    private IEnumerator changeSceneEnumerator()
    {
        fader.GetComponent<Animator> ().SetTrigger ("fadein");

        yield return new WaitForSeconds (1f);

        player.transform.rotation = respawnRotation;
        player.transform.position = respawnPosition.position;

        fader.GetComponent<Animator> ().SetTrigger ("fadeout");

        yield return new WaitForSeconds (0.01f);

        playerController.enabled = true;
        playerController.SetMouseCursor(true);
    }
}
