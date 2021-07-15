using System;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] Sounds;
    public GameObject AudioPlayer;

    public void PlaySound(string SoundName, Vector3 location)
    {
        GameObject Instance;
        Sound sound;

        sound= Array.Find(Sounds, Sound => Sound.name == SoundName);
        Instance=Instantiate(AudioPlayer);
        Instance.GetComponent<SoundPlayer>().PlaySound(sound.audio, location);
    }
}
