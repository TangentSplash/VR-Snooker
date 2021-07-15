using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    private bool PlayStart = false;
    private AudioSource audioSource;

    void Awake()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.volume = 0.5f;
        audioSource.Play();
        PlayStart = true;

    }


    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && PlayStart)
        {
            Destroy(gameObject);
        }
    }
}
