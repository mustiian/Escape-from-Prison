using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    private Color defaultColor = Color.black;

    private Image fadePanel;

    private bool isFadeOut;

    private bool isFadeIn;

    // Start is called before the first frame update
    void Start()
    {
        fadePanel = GetComponent<Image> ();   
    }

    public void FadeOut(float duration = 3, Color? color = null )
    {
        if (color == null)
            color = defaultColor;

        Color animColor = (Color)color;
        // Save original alpha of fadePanel
        animColor.a = fadePanel.color.a;
        fadePanel.color = animColor;

        if (!isFadeIn && !isFadeOut)
        {
            isFadeOut = true;
            StartCoroutine (FadeOutCoroutine (duration, animColor));
        }
    }

    public void FadeIn(float duration = 3, Color? color = null)
    {
        if (color == null)
            color = defaultColor;

        Color animColor = (Color)color;
        // Save original alpha of fadePanel
        animColor.a = fadePanel.color.a;
        fadePanel.color = animColor;

        if(!isFadeIn && !isFadeOut)
        {
            isFadeIn = true;
            StartCoroutine (FadeInCoroutine(duration, animColor));
        }
    }

    public void FadeInOut( float durationIn = 3, Color? colorIn = null,
                      float durationOut = 3, Color? colorOut = null, float fadeDelay = 5 )
    {
        if (colorIn == null)
            colorIn = defaultColor;

        if (colorOut == null)
            colorOut = defaultColor;

        Color animColorIn = (Color)colorIn;
        Color animColorOut = (Color)colorIn;

        // Save original alpha of fadePanel
        animColorIn.a = fadePanel.color.a;
        fadePanel.color = animColorIn;

        StartCoroutine (FadeInOutCoroutine (durationIn, animColorIn, durationOut, animColorOut, fadeDelay));
    }

    private IEnumerator FadeInOutCoroutine( float durationIn, Color colorIn,
                                           float durationOut, Color colorOut, float fadeDelay )
    {
        FadeIn (durationIn, colorIn);
        yield return new WaitForSeconds (fadeDelay + durationIn);
        FadeOut (durationOut, colorOut);
        yield return new WaitForSeconds (durationOut);
    }

    private IEnumerator FadeInCoroutine(float duration, Color color)
    {
        // Time, when the script was run
        float startTime = Time.time;
        // Starting image alpha
        float alpha = color.a;
        // Fading step (depends on fading duration)
        float step = 1;

        // Chages alpha channel every frame by step
        while (startTime + duration > Time.time)
        {
            step -= (1 / duration) * Time.deltaTime;
            color.a = Mathf.Lerp (1, alpha, step);
            fadePanel.color = color;

            yield return null;
        }

        isFadeIn = false;
    }

    private IEnumerator FadeOutCoroutine(float duration, Color color)
    {
        // Time, when the script was run
        float startTime = Time.time;
        // Starting image alpha
        float alpha = color.a;
        // Fading step (depends on fading duration)
        float step = 0;

        // Chages alpha channel every frame by step
        while (startTime + duration > Time.time)
        {
            step += (1 / duration) * Time.deltaTime;
            color.a = Mathf.Lerp (alpha, 0, step);
            fadePanel.color = color;

            yield return null;
        }

        isFadeOut = false;
    }
}
