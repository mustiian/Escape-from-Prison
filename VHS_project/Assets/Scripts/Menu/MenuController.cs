using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    public void NextLevel()
    {
        int thisLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisLevel + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
