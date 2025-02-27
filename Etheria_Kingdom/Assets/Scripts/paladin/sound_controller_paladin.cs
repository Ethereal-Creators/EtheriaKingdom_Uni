using UnityEngine;

public class audio_man_paladin : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip attaque;

    private void Start() // Now inside the class
    {
        musicSource.clip = attaque;
        musicSource.Play();
    }
}
