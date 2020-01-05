using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneWIthSound : Action
{
    public Scene SceneActivate;

    public AudioSource Audio;

    public AudioClip Clip;

    public float Delay;

    public override void Activate()
    {
        SceneActivate.ChangeScene(1, 8);
        StartCoroutine(SoundPlayCoroutine(Delay));
    }

    private IEnumerator SoundPlayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Audio.clip = Clip;
        Audio.Play();
    }
}
