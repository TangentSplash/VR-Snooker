using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SoundManager : MonoBehaviour
{
    public Sound[] Sounds;
    public GameObject AudioPlayer;
    public ActionBasedController[] Controllers;

    public void PlaySound(string SoundName, Vector3 location)
    {
        GameObject Instance;
        Sound sound;

        sound= Array.Find(Sounds, Sound => Sound.name == SoundName);
        Instance=Instantiate(AudioPlayer);
        Instance.GetComponent<SoundPlayer>().PlaySound(sound.audio, location);
    }

    public void PlayHaptic(int hand, float intensity, float duration)
    {
        Controllers[hand].SendHapticImpulse(intensity, duration);
    }
}
