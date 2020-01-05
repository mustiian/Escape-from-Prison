using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;
        public string Name;
    }

    public List<Sound> Sounds;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeSound("Cells");
    }

    public void ChangeSound(string Name)
    {
        Sounds.ForEach(delegate (Sound sound)
        {
            if (sound.Name == Name)
            {
                audioSource.clip = sound.clip;
                audioSource.Play();
            }
        });
    }
}
