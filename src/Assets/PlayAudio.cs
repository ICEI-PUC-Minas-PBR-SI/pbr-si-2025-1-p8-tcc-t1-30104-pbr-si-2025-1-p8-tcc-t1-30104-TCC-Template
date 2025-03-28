using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();


    }

    void Update()
    {

    }

    public void OnPlayAudio()
    {
        if (audioSource)
        {
            audioSource.Play();
        }
    }

}
