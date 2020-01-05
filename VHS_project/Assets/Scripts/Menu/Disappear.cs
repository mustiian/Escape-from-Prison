using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Disappear : MonoBehaviour
{
    public TextMeshProUGUI Text;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisappearCoroutine(10f));
    }

    private IEnumerator DisappearCoroutine(float duration)
    {
        yield return new WaitForSeconds(4f);

        float startTime = Time.time;
        float step = 1;

        while (startTime + duration > Time.time)
        {
            step -= (1 / duration) * Time.deltaTime;

            Text.alpha = Mathf.Lerp(0, Text.alpha, step);

            yield return null;
        }
    }
}
