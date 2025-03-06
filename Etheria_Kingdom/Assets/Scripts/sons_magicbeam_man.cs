using UnityEngine;
using static Unity.VisualScripting.Member;

public class audio_man_mage : MonoBehaviour
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
        musicSource.pitch = Random.Range(0.8f, 1.2f);
    }
}