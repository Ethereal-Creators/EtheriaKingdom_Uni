using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip waveWon;
    public AudioClip waveLost;

    private void Start() // Now inside the class
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
