using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class Scene : MonoBehaviour
{
    public string Name;
    public Transform respawnPosition;
    public Quaternion respawnRotation;

    private SoundController soundController;
    private GameObject player;
    private GameObject fader;
    private FirstPersonController playerController;
    private GameObject sceneGameObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        soundController = GameObject.FindGameObjectWithTag("soundController").GetComponent<SoundController>();
        playerController = player.GetComponent<FirstPersonController> ();
        fader = GameObject.FindGameObjectWithTag ("Fader");
        sceneGameObject = transform.GetChild (0).gameObject;
    }

    public void ChangeScene(float delayIn = 1f, float delayOut = 1f)
    {
        Debug.Log("Change scene" + Name);
        StartCoroutine (changeSceneEnumerator (delayIn, delayOut));
    }

    public void ChangeScene()
    {
        StartCoroutine(changeSceneEnumerator(1f, 1f));
    }

    private IEnumerator changeSceneEnumerator(float delayIn, float delayOut)
    {
        fader.GetComponent<Animator> ().SetTrigger ("fadein");

        yield return new WaitForSeconds (delayIn);

        playerController.enabled = false;
        player.transform.rotation = respawnRotation;
        player.transform.position = respawnPosition.position;
        soundController.ChangeSound(Name);

        yield return new WaitForSeconds (delayOut);

        fader.GetComponent<Animator> ().SetTrigger ("fadeout");

        playerController.enabled = true;
        playerController.SetMouseCursor(true);
    }
}
