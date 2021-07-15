using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    bool Started = false;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Started && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip sound, Vector3 location)
    {
        transform.position = location;
        audioSource.clip=sound;
        Started = true;
        audioSource.Play();
    }
}
