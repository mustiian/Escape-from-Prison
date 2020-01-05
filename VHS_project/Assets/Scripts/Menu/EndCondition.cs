using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition : MonoBehaviour
{
    private GameObject fader;

    private void Start()
    {
        fader = GameObject.FindGameObjectWithTag("Fader");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Inventory.instance.ActiveItem != null)
                if (Inventory.instance.ActiveItem.Type == ItemType.Map)
                    StartCoroutine(ChangeSceneCoroutine(3f));
        }
    }

    private IEnumerator ChangeSceneCoroutine(float duration)
    {
        fader.GetComponent<Animator>().SetTrigger("fadein");

        yield return new WaitForSeconds(duration);

        int thisLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisLevel - 1);
    }
}
