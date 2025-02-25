using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class scriptSoundPlace : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    public List<AudioClip> clips = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
        this.gameObject.GetComponent<AudioSource>().enabled = true;
    }

    void OnEnable()
    {
        source = GetComponent<AudioSource>();
        source.enabled = true;
        Debug.Log("Active sound");
        if (source != null)
        {
            // Set a random pitch between 0.8f and 1.2f (you can adjust these values as needed)
            int randomClipIndex = Random.Range(0, clips.Count);
            source.PlayOneShot(clips[randomClipIndex]);
        }
    }

    private void OnDisable()
    {
        source = GetComponent<AudioSource>();
        source.enabled = true;
        Debug.Log("Active sound");
        if (source != null)
        {
            // Set a random pitch between 0.8f and 1.2f (you can adjust these values as needed)
            int randomClipIndex = Random.Range(0, clips.Count);
            source.PlayOneShot(clips[randomClipIndex]);
        }
    }
}
