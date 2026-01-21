using UnityEngine;

public class BGMManager : MonoBehaviour
{
public AudioClip gameMusic;

private AudioSource audioSource;

void Awake () 
{
    _audioSource = GetComponent<AudioSource>();
}

void Start ()
{
    StartBGM(); 
}

void StartBGM() 
{
   // _audioSource.loop = true;
    _audioSource.clip = gameMusic;
    _audioSource.Play();

    //_audioSource.Pause(); 
    //_audioSource.Stop();
}
}
